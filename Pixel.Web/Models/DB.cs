using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pixel.Web.DB
{

    [PetaPoco.TableName("Providers")]
    [PetaPoco.PrimaryKey("id")]
    public class pixProviders
    {
        public int id { get; set; }
        public string name { get; set; }
        public string pixel_url { get; set; }
        public string method { get; set; }
        public Boolean isActive{ get; set; }
        public string param1 { get; set; }
        public string param2 { get; set; }
        public string param3 { get; set; }
        public short sendResponseEvery { get; set; }
        public string pixel_url_Text2Replace { get; set; }
        public string pcmac { get; set; }
        

        
    }

    [PetaPoco.TableName("Requests")]
    [PetaPoco.PrimaryKey("id", autoIncrement = true)]
    
    public class pixRequests
    {
        public long id { get; set; }
        public Guid reqGuid{ get; set; }
        public int providerid { get; set; }
        public int pageid { get; set; }
        public string full_url { get; set; }
        public string redirect_to { get; set; }
        public string param1 { get; set; }
        public string param2 { get; set; }
        public string param3 { get; set; }
        public string user_ip { get; set; }
        public string platform { get; set; }
        public string countryCode { get; set; }
    }


    [PetaPoco.TableName("Responses")]
    [PetaPoco.PrimaryKey("id", autoIncrement = true)]
    public class pixResponses
    {
        public long id { get; set; }
        public Guid resGuid { get; set; }
        public string full_url { get; set; }
        public string response_url { get; set; }
        public string user_ip { get; set; }
        public Boolean sentToProvider { get; set; }
        public string platform { get; set; }
        public string countryCode { get; set; }
        public int providerid { get; set; }
        public int pageid { get; set; }
      }

    [PetaPoco.TableName("LandingPages")]
    [PetaPoco.PrimaryKey("id")]
    public class pixLandingPages
    {
        public long id { get; set; }
        
        public string name { get; set; }
        public string url { get; set; }
        public Boolean isActive{ get; set; }
        public string pcmac { get; set; }
        
   
    }


    [PetaPoco.TableName("LandingPagesMask")]
    [PetaPoco.PrimaryKey("id")]
    public class pixLandingPagesMask
    {
        public int id { get; set; }
        public int providerid { get; set; }
        public int pageid_origin { get; set; }
        public int pageid_redirectTo { get; set; }
    }

    [PetaPoco.TableName("LandingPagesMaskByGEO")]
    [PetaPoco.PrimaryKey("id", autoIncrement = true)]
    public class pixPageRedirectionByGEO
    {
        public int id { get; set; }
        public int providerid { get; set; }
        public int pageid_origin { get; set; }
        public int pageid_redirectTo { get; set; }
        public string countryCode { get; set; }

    }
    
    [PetaPoco.TableName("LandingPages_X_Mask")]
    [PetaPoco.PrimaryKey("id")]
    public class pixLandingPages_X_Mask
    {
        public int id { get; set; }
        public int providerid { get; set; }
        public int pageid { get; set; }
        public string countryCode { get; set; }
        public int sendResponseEvery_x { get; set; }
    }


    [PetaPoco.TableName("LandingPagesByGEO")]
    [PetaPoco.PrimaryKey("pageid,countryCode")]
    public class pixLandingPagesByGEO
    {
        public int pageid { get; set; }
        public string countryCode { get; set; }
        public string url { get; set; }

    }
     [PetaPoco.TableName("paidGEO")]
    [PetaPoco.PrimaryKey("id")]
    public class pixPaidGEO
    {
        public int id { get; set; }
        public string GEO { get; set; }
    }

    

    [PetaPoco.TableName("ResponseError")]
    [PetaPoco.PrimaryKey("id")]
    public class pixResponseError
    {
        public long id { get; set; }
        public string url { get; set; }

    }

    [PetaPoco.TableName("pixMonitor")]
    [PetaPoco.PrimaryKey("id", autoIncrement = true)]

    public class pixMonitor
    {
        public long id { get; set; }
        public long id2 { get; set; }

        //public DateTime Monitor_Date { get; set; }
        
    }

}