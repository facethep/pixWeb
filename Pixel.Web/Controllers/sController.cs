using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pixel.Web.DB;
using Pixel.Web.Models;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

/*
 The Pixel from the DLM will send data to here 
 
 */

namespace Pixel.Web.Controllers
{
    public class sController : ApiController
    {
        //initialize logger
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private pixProviders tmpProvider;
        private int intRequestPageId;
        private  pixRequests tmpRequest;
        // GET: api/s
        //this function will be called when installaiton is finished and we are getting a call from th DLM
       
        
        public string  Get()
        {
            string sUID, fullUrl, pixelURL,  retValue, requestCountryCode;
            string ip_address, res_countryCode;
            ip_address = res_countryCode = requestCountryCode = string.Empty;
            Guid tmpGuid;
            Int32 intProviderId;

           retValue = "OK";

           fullUrl = Request.RequestUri.AbsoluteUri;
           
            //Get uid from request
           sUID = Pixel.Web.Models.pixHelpers.GetQueryString(Request, "uid");

           log.Info("Got request from DLM: " + fullUrl );

           // if sUID cannot be converted to GUID - no point in moving forward
           if (Guid.TryParse(sUID, out tmpGuid))
           {

               //get requet with equivilant sUID
                var db = new PetaPoco.Database("myConnectionString");
                 tmpRequest = db.SingleOrDefault<pixRequests>("SELECT * FROM requests WHERE reqGuid=@0", tmpGuid.ToString());

                if (tmpRequest != null)
                {

                    //get providerID from request
                    intProviderId = tmpRequest.providerid;
                    intRequestPageId = tmpRequest.pageid;
                    requestCountryCode = tmpRequest.countryCode;
                    
                   //Get full provider record from cache according to provifderID
                    tmpProvider = SettingsCache.GetProvider(intProviderId);

                    if ( tmpProvider!= null)
                    {
                        log.Info("Found provider: " + tmpProvider.id.ToString() + " ,building response URL");
                       
                        //getting server to server url and changing return param value
                        if (intProviderId == 1008) {
                            string countryCode = tmpRequest.countryCode.ToUpper();
                            tmpProvider.pixel_url = getMondoS2SURL(countryCode);
                            
                        }

                        
                        pixelURL = buildS2SURL(tmpProvider, tmpRequest);

                  

                        //set up parameters for responses handel
                        var myResponse = new pixResponses();
                        myResponse.sentToProvider = false;

                        // check if we need to send response to provider 
                        //if (blnSendPixel() && sendResponseByCountry(intRequestPageId, requestCountryCode))
                        string country_Code = tmpRequest.countryCode;
                        if (blnSendPixel() && Pixel.Web.Models.SettingsCache.GetPaidGEO(country_Code))
                        {
                            try
                            {
                                //send actual response only in production environment
                                if (Pixel.Web.Models.SettingsCache.SEND_RESPONSE)
                                {
                                    //send response to provider
                                    WebClient proxy = new WebClient();
                                    log.Info("Sending pixel response to Provider: " + pixelURL);
                                    Thread.Sleep(300);
                                    var response = proxy.DownloadString(pixelURL);
                                    proxy.Dispose();
                                    myResponse.sentToProvider = true;
                                
                                }

                            }
                            catch (Exception e)
                            {
                                log.Fatal("Error in sending response to provider: " + pixelURL, e);
                                retValue = "ERROR 2";
                            }
                        } 

                       
                           // save all good response to database

                                myResponse.full_url = fullUrl;
                                myResponse.response_url = pixelURL;
                                myResponse.resGuid = tmpRequest.reqGuid;
                                myResponse.providerid = tmpRequest.providerid;
                                myResponse.pageid = tmpRequest.pageid;
                                myResponse.countryCode = tmpRequest.countryCode;
                        
                           
                                try
                                        {
                                            db.Insert(myResponse);
                                            db.CloseSharedConnection();
                                        }
                                catch (Exception e)
                                        {
                                            db.CloseSharedConnection();
                                            log.Fatal("Could not insert Response to database with url: " + fullUrl, e);
                                            retValue = "ERROR 1";
                                        }

//****************************************************************************************************************************
                    }

                    else
                    {
                        log.Fatal("Counld not find provider id in: " + fullUrl +", notification will not be sent");
                        retValue = "ERROR 3";                                           
                    }
                    
                }

                else //a is null
                {

                    log.Fatal("could not find a request in the database with the following GUID: " + sUID);
                    retValue = "ERROR 4";
                }

              
            }

            else
            {
                log.Fatal("The uid we got from DLM is empty or cannot be converted to guid : " + fullUrl);
                insertErrorResponse(fullUrl);
                retValue = "ERROR 5";

            }
           return retValue ;
        }




        private string getMondoS2SURL(string countryCode)
        {
            //TODO: 1008 mondu has a pixel per country -- WILL NEED TO MOVE TO BETTER CODE

            string pixel_url;
            switch (countryCode)
            {
                case "US":
                    pixel_url = "http://www.mlinktracker.com/lead/e2c4x26494x2w2/&cookieid=[TRID]";
                    break;
                case "UK":
                    pixel_url = "http://www.mlinktracker.com/lead/e2c4x26494x2x2/&cookieid=[TRID]";
                    break;
                case "FR":
                    pixel_url = "http://www.mlinktracker.com/lead/e2c4x26494x2y2/&cookieid=[TRID]";
                    break;
                case "DE":
                    pixel_url = "http://www.mlinktracker.com/lead/e2c4x26494x2y2/&cookieid=[TRID]";
                    break;
                case "BR":
                    pixel_url = "http://www.mlinktracker.com/lead/e2c4x26494x2v2/&cookieid=[TRID]";
                    break;
                default:
                    pixel_url = "http://www.mlinktracker.com/lead/e2c4x26494x2w2/&cookieid=[TRID]";
                    log.Error("sController - got unidentified country for provider 1008 response by country: " + countryCode);
                    break;
            }
            return pixel_url;
        }

        // will check if the country the request came from is one that has a landing page
        // if not we should not send response pixel
       /* private Boolean sendResponseByCountry(int intPageId, string countryCode)
        {
           
            Boolean retVal = true;
            if (SettingsCache.checkLandingPageByGeo(intPageId))
            {
                pixLandingPagesByGEO tmpPage = SettingsCache.GetPageByGEO(intPageId, countryCode);
                if (tmpPage == null) {
                    retVal = false;
                }
            }

            log.Info("sendResponseByCountry = " + retVal.ToString());
            return retVal;


        }
        */
        private string buildS2SURL(pixProviders provider, pixRequests req) {
      
            //split string to replace vro mprovider
            // for each one 
            //take paramX from request and replace in pixel URL from provider
              List<string> paramsToReplace;
            paramsToReplace = provider.pixel_url_Text2Replace.Split(',').ToList();
            string pixelURL = provider.pixel_url;
            int i=1;

            

            try
            {
                foreach (string value in paramsToReplace)
                {
                    if (value.Contains("#CC#") ) //replacing countyrcode and any other system variables we want :-) just make sure to put in ## and implement code
                    {
                        
                        pixelURL = pixelURL.Replace(value, req.countryCode);
                    }
                    else if (value.Contains("#UID#"))
                    {
                        pixelURL = pixelURL.Replace(value, req.reqGuid.ToString());

                    }

                    else
                    { 
                        pixelURL = pixelURL.Replace(value, Convert.ToString(req.GetType().GetProperty("param" + i.ToString()).GetValue(req, null)));
                        i += 1;
                    }
                   
                }
            }
            catch (Exception e){
                log.Fatal("Faild to build pixel URL from request ID: " + req.id.ToString(),e);
            }


            return pixelURL;
            
        }

     
        private  Boolean SendIfGeneo(Boolean isGeneo, string countryCode){
            /* 
               Geneio    US     Result
             *  False   True    True
             *  True    False   False
             *  True    True    True
             *  False   False   True
             */

           
            if (isGeneo && countryCode.ToUpper() !="US"){ return false;}
            return true;

        }
   
        // Add error response to database
        private void insertErrorResponse(string fullUrl)
        {
                var db = new PetaPoco.Database("myConnectionString");
                var responseError = new pixResponseError();
                responseError.url = fullUrl;
                try
                {
                    db.Insert(responseError);
                    db.CloseSharedConnection();

                }
                catch (Exception e)  {
                    db.CloseSharedConnection();
                    log.Fatal("Error inserting response error to DataBase", e);
                }                

        }

       
        //this prosudere will determine if we need to send pixel accoording to provider data from database
        private Boolean  blnSendPixel()
        {
            //building application counter name for each provider to count number of responses

            string appKey = string.Empty; 
            int responseCounterValue, SendResponseEvery;
           
            string _countryCode = tmpRequest.countryCode;
            int _pageid = tmpRequest.pageid;
            int _providerid = tmpRequest.providerid;
            bool retVal = false;
             
            //how often should we send responses to this provider
            SendResponseEvery = tmpProvider.sendResponseEvery;

            
            // try to get a specific X for this page, if it does not exist it will retur -1 and we will get back to the default
            int i = SettingsCache.getGeoX(_pageid, _countryCode, _providerid);

            if (i > -1)
            {
              //  appKey =  "provider_" + tmpRequest.providerid.ToString() + "_page_" + _pageid.ToString() + "_" + _countryCode + "_RC";
                SendResponseEvery = i;

            }
         //   else{
         //       appKey = "provider_" + tmpRequest.providerid.ToString() + "_RC";
         //       }

            appKey = "provider_" + tmpRequest.providerid.ToString() + "_page_" + _pageid.ToString() + "_" + _countryCode + "_RC";
            responseCounterValue = 0;

          
            if (SendResponseEvery != 1) // if we need to send each time there is not use to check.
            {

                try
                {

                    //Get the current X from database and 
                    var oVar_responseCounterValue = new SqlParameter("@retval", SqlDbType.Int);
                    oVar_responseCounterValue.Direction = ParameterDirection.Output;
                    oVar_responseCounterValue.Size = 3;

                    var s = PetaPoco.Sql.Builder.Append(";EXEC SP_InstallX @0, @1 OUTPUT", appKey, oVar_responseCounterValue);
                    new PetaPoco.Database("myConnectionString").Execute(s);
                    responseCounterValue = Convert.ToInt32(oVar_responseCounterValue.Value);
                    log.Info("appKey: " + appKey + " = " + responseCounterValue.ToString());
                    

                }
                catch (Exception e)
                {
                   
                    log.Fatal("Erorr executing SP_InstallX for: " + appKey, e);
                     return true;
                }

                
                retVal  =  !((responseCounterValue % SendResponseEvery) != 0);

                //log.Error("SENDING PIXEL STATUS: " + appKey + ", responseCounterValuec=" + responseCounterValue.ToString() + ", SendResponseEvery = " + SendResponseEvery.ToString() + ", STATUS= " + retVal.ToString()) ;

                log.Info("Need to send pixel to provider: " + appKey + " = " + retVal.ToString());
                return retVal;
            }
            return true;
           
        }
 

        // GET: api/s/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/s
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/s/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/s/5
        public void Delete(int id)
        {
        }
    }
}
