$(document).ready(function(){
	var TIME_OUT = 300;
	
	function smallScreen() {
		var tmp = ($(window).width() < 1001) ? true : false;
		return tmp;
	}
	
	(function(){
		var $header = $('#header'),
			$menu = $header.find('nav').children('ul'),
			$btnMenu = $header.find('.btn-menu'),
			$parent = $header.find('.contact-box'),
			$btn = $parent.find('.btn-contact'),
			$entity = $parent.find('.entity'),
			OFFSET = 224,
			OPEN_CLASS = 'opened';
		
		$btn.click(function(){
			var offset = ($parent.hasClass(OPEN_CLASS)) ? -OFFSET : 0;
			
			if (smallScreen()) {
				$entity.animate({'left' : offset}, TIME_OUT, function() {
					$parent.toggleClass(OPEN_CLASS);
				});
			}
			else {
				$parent.animate({'left' : offset}, TIME_OUT, function() {
					$parent.toggleClass(OPEN_CLASS);
				});
			}
			return false;
		});
		
		$btnMenu.click(function() {
			$menu.slideToggle(TIME_OUT);
			return false;
		});
		
		function checkThis() {
			var scroll_timer,
				displayed = false,
				$window = $(window),
				top;
				
			if (smallScreen()) {
				top = $('.area').position().top;
				
				window.clearTimeout(scroll_timer);
				scroll_timer = window.setTimeout(function(){
					if ($window.scrollTop() <= top) {
						displayed = false;
						$header.removeClass('sticky');
					}
					else 
						if (displayed == false) {
							displayed = true;
							$header.addClass('sticky');
						}
				}, 100);
			}
			else {
				$header.removeClass('sticky');
			}
		}
		checkThis();
		
		$(window).resize(function() {
			checkThis();
		});
		$(window).scroll(function() {
			checkThis();
		});
	})();
	
	$('.btn-top').click(function(){
		$(window).scrollTo(0, TIME_OUT*2);
		return false;
	});
	
	$('.data').each(function(){
		$(this).find('tbody').find('tr:last-child').addClass('last-child');
	});
	
	// popup
	$('.open-popup').fancybox({
		'wrapCSS' : 'fancybox-popup',
		'padding' : 0,
		'closeBtn' : false,
		'modal' : true
	});
	(function() {
		var $popup = $('.popup'),
			$btnClose = $popup.find('.btn-close'),
			$btnExit = $popup.find('.btn-exit'),
			$btnContinue = $popup.find('.btn-continue');
		
		setTimeout(function() {
			$popup.find('.confirm-box').hide();
		}, TIME_OUT*2);
		
		$btnClose.click(function(){
			var $confirm = $(this).closest('.popup').find('.confirm-box');
			
			if ($confirm.length > 0) {
				$confirm.fadeIn(TIME_OUT);
			}
			else {
				$.fancybox.close();
			}
			return false;
		});
		
		$btnExit.click(function() {
			$(this).closest('.confirm-box').fadeOut(TIME_OUT);
			$.fancybox.close();
			return false;
		});
		
		$btnContinue.click(function() {
			$(this).closest('.confirm-box').fadeOut(TIME_OUT);
			return false;
		});
	})();
	// accordion
	/*$('.link-list').each(function() {
		var $list = $(this),
			activeClass = 'active',
			target = '.holder';
		
		$list.find(target).hide();
		
		if (!($list.children('li').children('a.' + activeClass).length > 0)) {
			$list.children('li:first-child').children('a').addClass(activeClass);
		}
		
		$list.children('li').children('a.' + activeClass).closest('li').find(target).show();
		
		$list.children('li').children('a').click(function() {
			var $this = $(this),
				$active = $this.closest('ul').children('li').children('a.' + activeClass).not($this);
				
			$active.removeClass(activeClass).closest('li').find(target).slideUp(TIME_OUT);
			$this.closest('li').find(target).slideToggle(TIME_OUT, function() {
				$this.toggleClass(activeClass);
			});
			return false;
		});
	});*/
	
	customForm.lib.domReady(function(){
		customForm.customForms.replaceAll();
	});
	
	
	// new
	// validate
	$('#form-1').validate({
		showErrors: function(errorMap, errorList) {
			$(".error-place").html("יש למלא כתובת דואר").fadeIn(TIME_OUT);
			
			for ( var i = 0; this.errorList[i]; i++ ) {
				var error = this.errorList[i];
				this.settings.highlight && this.settings.highlight.call( this, error.element, this.settings.errorClass, this.settings.validClass );
			}
			if( this.errorList.length ) {
				this.toShow = this.toShow.add( this.containers );
			}
			if (this.settings.unhighlight) {
				for ( var i = 0, elements = this.validElements(); elements[i]; i++ ) {
					this.settings.unhighlight.call( this, elements[i], this.settings.errorClass, this.settings.validClass );
				}
			}
			this.toHide = this.toHide.not( this.toShow );
			this.hideErrors();
			this.addWrapper( this.toShow ).show();
		},
		submitHandler: function() {
			$(".error-place").hide();
			$.fancybox({
				'wrapCSS' : 'fancybox-thx',
				'padding' : 0,
				'closeBtn' : false,
				'href' : '#small-popup-1'
			});
		},
		rules: {
			name: {
				required: true,
				minlength: 2
			},
			phone: {
				required: true,
				minlength: 6
			},
			email: {
				required: true,
				email: true
			}
		}
	});
	// end new
	
	
	if (window.PIE) {
		$('.ie-fix, #header .contact-box .entity, #header .contact-box .text input, #header .contact-box .text textarea, .select-area, .box > header, .popup .text input').each(function() {
			PIE.attach(this);
		});
	}
});


// clear inputs on focus
function initInputs() {
	// replace options
	var opt = {
		clearInputs: true,
		clearTextareas: true,
		clearPasswords: true
	}
	// collect all items
	var inputs = [].concat(
		PlaceholderInput.convertToArray(document.getElementsByTagName('input')),
		PlaceholderInput.convertToArray(document.getElementsByTagName('textarea'))
	);
	// apply placeholder class on inputs
	for(var i = 0; i < inputs.length; i++) {
		if(inputs[i].className.indexOf('default') < 0) {
			var inputType = PlaceholderInput.getInputType(inputs[i]);
			if((opt.clearInputs && inputType === 'text') ||
				(opt.clearTextareas && inputType === 'textarea') || 
				(opt.clearTextareas && inputType === 'email') || 
				(opt.clearPasswords && inputType === 'password')
			) {
				new PlaceholderInput({
					element:inputs[i],
					wrapWithElement:false,
					showUntilTyping:false,
					getParentByClass:false,
					placeholderAttr:'value'
				});
			}
		}
	}
}

// input type placeholder class
;(function(){
	PlaceholderInput = function() {
		this.options = {
			element:null,
			showUntilTyping:false,
			wrapWithElement:false,
			getParentByClass:false,
			placeholderAttr:'value',
			inputFocusClass:'focus',
			inputActiveClass:'text-active',
			parentFocusClass:'parent-focus',
			parentActiveClass:'parent-active',
			labelFocusClass:'label-focus',
			labelActiveClass:'label-active',
			fakeElementClass:'input-placeholder-text'
		}
		this.init.apply(this,arguments);
	}
	PlaceholderInput.convertToArray = function(collection) {
		var arr = [];
		for (var i = 0, ref = arr.length = collection.length; i < ref; i++) {
		 arr[i] = collection[i];
		}
		return arr;
	}
	PlaceholderInput.getInputType = function(input) {
		return (input.type ? input.type : input.tagName).toLowerCase();
	}
	PlaceholderInput.prototype = {
		init: function(opt) {
			this.setOptions(opt);
			if(this.element && this.element.PlaceholderInst) {
				this.element.PlaceholderInst.refreshClasses();
			} else {
				this.element.PlaceholderInst = this;
				if(this.elementType == 'text' || this.elementType == 'password' || this.elementType == 'email' || this.elementType == 'textarea') {
					this.initElements();
					this.attachEvents();
					this.refreshClasses();
				}
			}
		},
		setOptions: function(opt) {
			for(var p in opt) {
				if(opt.hasOwnProperty(p)) {
					this.options[p] = opt[p];
				}
			}
			if(this.options.element) {
				this.element = this.options.element;
				this.elementType = PlaceholderInput.getInputType(this.element);
				this.wrapWithElement = (this.elementType === 'password' || this.options.showUntilTyping ? true : this.options.wrapWithElement);
				this.setOrigValue( this.options.placeholderAttr == 'value' ? this.element.defaultValue : this.element.getAttribute(this.options.placeholderAttr) );
			}
		},
		setOrigValue: function(value) {
			this.origValue = value;
		},
		initElements: function() {
			// create fake element if needed
			if(this.wrapWithElement) {
				this.element.value = '';
				this.element.removeAttribute(this.options.placeholderAttr);
				this.fakeElement = document.createElement('span');
				this.fakeElement.className = this.options.fakeElementClass;
				this.fakeElement.innerHTML += this.origValue;
				this.fakeElement.style.color = getStyle(this.element, 'color');
				this.fakeElement.style.position = 'absolute';
				this.element.parentNode.insertBefore(this.fakeElement, this.element);
			}
			// get input label
			if(this.element.id) {
				this.labels = document.getElementsByTagName('label');
				for(var i = 0; i < this.labels.length; i++) {
					if(this.labels[i].htmlFor === this.element.id) {
						this.labelFor = this.labels[i];
						break;
					}
				}
			}
			// get parent node (or parentNode by className)
			this.elementParent = this.element.parentNode;
			if(typeof this.options.parentByClass === 'string') {
				var el = this.element;
				while(el.parentNode) {
					if(hasClass(el.parentNode, this.options.parentByClass)) {
						this.elementParent = el.parentNode;
						break;
					} else {
						el = el.parentNode;
					}
				}
			}
		},
		attachEvents: function() {
			this.element.onfocus = bindScope(this.focusHandler, this);
			this.element.onblur = bindScope(this.blurHandler, this);
			if(this.options.showUntilTyping) {
				this.element.onkeydown = bindScope(this.typingHandler, this);
				this.element.onpaste = bindScope(this.typingHandler, this);
			}
			if(this.wrapWithElement) this.fakeElement.onclick = bindScope(this.focusSetter, this);
		},
		togglePlaceholderText: function(state) {
			if(this.wrapWithElement) {
				this.fakeElement.style.display = state ? '' : 'none';
			} else {
				this.element.value = state ? this.origValue : '';
			}
		},
		focusSetter: function() {
			this.element.focus();
		},
		focusHandler: function() {
			this.focused = true;
			if(!this.element.value.length || this.element.value === this.origValue) {
				if(!this.options.showUntilTyping) {
					this.togglePlaceholderText(false);
					this.refreshClasses();
				}
			}
			this.refreshClasses();
		},
		blurHandler: function() {
			this.focused = false;
			if(!this.element.value.length || this.element.value === this.origValue) {
				this.togglePlaceholderText(true);
			}
			this.refreshClasses();
		},
		typingHandler: function() {
			setTimeout(bindScope(function(){
				if(this.element.value.length) {
					this.togglePlaceholderText(false);
					this.refreshClasses();
				}
			},this), 10);
		},
		refreshClasses: function() {
			this.textActive = this.focused || (this.element.value.length && this.element.value !== this.origValue);
			this.setStateClass(this.element, this.options.inputFocusClass,this.focused);
			this.setStateClass(this.elementParent.parentNode, this.options.parentFocusClass,this.focused);
			this.setStateClass(this.labelFor, this.options.labelFocusClass,this.focused);
			this.setStateClass(this.element, this.options.inputActiveClass, this.textActive);
			this.setStateClass(this.elementParent, this.options.parentActiveClass, this.textActive);
			this.setStateClass(this.labelFor, this.options.labelActiveClass, this.textActive);
		},
		setStateClass: function(el,cls,state) {
			if(!el) return; else if(state) addClass(el,cls); else removeClass(el,cls);
		}
	}
	
	// utility functions
	function hasClass(el,cls) {
		return el.className ? el.className.match(new RegExp('(\\s|^)'+cls+'(\\s|$)')) : false;
	}
	function addClass(el,cls) {
		if (!hasClass(el,cls)) el.className += " "+cls;
	}
	function removeClass(el,cls) {
		if (hasClass(el,cls)) {el.className=el.className.replace(new RegExp('(\\s|^)'+cls+'(\\s|$)'),' ');}
	}
	function bindScope(f, scope) {
		return function() {return f.apply(scope, arguments)}
	}
	function getStyle(el, prop) {
		if (document.defaultView && document.defaultView.getComputedStyle) {
			return document.defaultView.getComputedStyle(el, null)[prop];
		} else if (el.currentStyle) {
			return el.currentStyle[prop];
		} else {
			return el.style[prop];
		}
	}
}());

if (window.addEventListener) window.addEventListener("load", initInputs, false);
else if (window.attachEvent) window.attachEvent("onload", initInputs);