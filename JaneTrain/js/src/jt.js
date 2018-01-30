jQuery( document ).ready(function() {
    JT.init();
});

var JT = JT || {};

JT.Vars = {
    body: '',
    bodyWrapper: '',
    collapseLink: '',
    header: '',
    stickyContact: '',
    stickyNav: ''
};

JT.init = function(){
    // set up global vars for optimization / speed
    JT.Vars.htmlbody = $('html, body');
    JT.Vars.body = $('body');
    JT.Vars.bodyWrapper = $('#body-wrapper');
    JT.Vars.header = $('header');


    // set up logic to detect mobile, tablet, desk widths
    JT.Widths.init();

    $(window).resize(function() {
        JT.Widths.widthCheck();
    });

    // set up footer behaviors
    JT.Footer.setup();


    if(JT.Vars.body.attr("id") === "homepage"){
        JT.Homepage.init();
    }

};

// Check for widths, add hooks for CSS / JS use
JT.Widths = {
    // if changing these width values, 
    // please also update the matching CSS vars
    // in the _vars.scss file.
    mobileStart: 320,
    tabletStart: 601,
    deskStart: 1200,

    isMobile : false,
    isTablet : false,
    isDesk: false,
    init: function(){
        var windowWidth = window.innerWidth,
            mobileEnd = (JT.Widths.tabletStart - 1),
            tabletEnd = (JT.Widths.deskStart - 1);

        if(windowWidth <= mobileEnd){
           JT.Mobile.setup();

            // hooks
            JT.Vars.body.addClass('mobile');
            JT.Widths.isMobile = true;

        }else if((windowWidth >= JT.Widths.tabletStart) && (windowWidth <= tabletEnd)){

            JT.Tablet.setup();

            // hooks
            JT.Vars.body.addClass('tablet');
            JT.Widths.isTablet = true;

        }else{

            JT.Desk.setup();

            // hooks
            JT.Vars.body.addClass('desk');
            JT.Widths.isDesk = true;
        }
    },
    widthCheck: function() {
        var windowWidth = window.innerWidth,
            mobileEnd = (JT.Widths.tabletStart - 1),
            tabletEnd = (JT.Widths.deskStart - 1);
        if(windowWidth <= mobileEnd){
            if(JT.Widths.isTablet){
                JT.Tablet.teardown();
            }
            if(JT.Widths.isDesk){
                JT.Desk.teardown();
            }
            if(!JT.Widths.isMobile){
                JT.Mobile.setup();
            }
            // hooks
            JT.Vars.body.addClass('mobile').removeClass('tablet').removeClass('desk');
            JT.Widths.isMobile = true;
            JT.Widths.isTablet = false;
            JT.Widths.isDesk = false;

        }else if((windowWidth >= JT.Widths.tabletStart) && (windowWidth <= tabletEnd)){
            if(JT.Widths.isMobile){
                JT.Mobile.teardown();
            }
            if(JT.Widths.isDesk){
                JT.Desk.teardown();
            }
            if(!JT.Widths.isTablet){
                JT.Tablet.setup();
            }
            // hooks
            JT.Vars.body.addClass('tablet').removeClass('mobile').removeClass('desk');
            JT.Widths.isMobile = false;
            JT.Widths.isTablet = true;
            JT.Widths.isDesk = false;

        }else{
            if(JT.Widths.isMobile){
                JT.Mobile.teardown();
            }
            if(JT.Widths.isTablet){
                JT.Tablet.teardown();
            }
            if(!JT.Widths.isDesk){
                JT.Desk.setup();
            }
            // hooks
            JT.Vars.body.addClass('desk').removeClass('tablet').removeClass('mobile');
            JT.Widths.isMobile = false;
            JT.Widths.isTablet = false;
            JT.Widths.isDesk = true;
        }


    }
};

JT.Mobile =  {
    navInit: false,
    mobileOnlyCarousels : '',
    listCarousels: '',
    setup:function() {
        // set up mobile-exclusive behavior

        // set up back-to-top arrow on all pages
        $('#back-to-top-arrow').addClass('visible').click(function(e){
           e.preventDefault();
           if (window.pageYOffset >= 20){
               JT.Vars.htmlbody.animate({
                   scrollTop: 0
               }, 750);
           }
        });


    },
    teardown:function() {
        // remove mobile-exclusive behavior

        // tear down back-to-top arrow
        $('#back-to-top-arrow').removeClass('visible').unbind();

    }
};

JT.Tablet =  {
    setup:function() {
        // set up tablet-exclusive behavior

    },
    teardown:function() {
        // remove tablet-exclusive behavior
    }
};

JT.Desk =  {
    navInit : false,
    setup:function() {
        // set up desk-exclusive behavior

    },
    teardown:function() {
        // remove desk-exclusive behavior
    }
};

JT.Footer = {
    setup: function(){
        // clear email address field on focus
        $('#footer-email-address').focus(function(){
            var $this = $(this);
            JT.Clearfield($this, 'email address');
        });
    }
};

JT.Clearfield = function($field, defaultValue){
    if($field.val() === defaultValue){
        $field.val('');
    }
};


JT.Homepage = {
    init: function () {
        JT.Homepage.marqueeHelper();
    },
    marqueeHelper: function(){
        // first item in marquee should not slide;
        // this animates its neighbor to the right
        $('#locations div').first().hover(
            function() {
                $(this).next().addClass( "revealRight" );
            }, function() {
                $(this).next().removeClass( "revealRight" );
            }
        );
        $('#home-marquee a').click(function(e){
            e.preventDefault();
            var href = $(this).attr('href');
            JT.Scroll.pageScroller($(href), 60);
        });
    }
};

JT.Overlay ={
    isOpen: false,
    close:function(openClass){
        JT.Vars.bodyWrapper.css('padding-top','0');
        JT.Vars.body.removeClass(JT.Overlay.openClass);
        JT.Overlay.isOpen = false;
    },
    closeBinder: function($closeButton, closeFunction ){
        $closeButton.click(function (e) {
            e.preventDefault();
            closeFunction();
        });

        $('#overlay').click(function(e){
            e.preventDefault();
            closeFunction();
        });
    },
    open:function(openClass){
        var headerHeight = JT.Vars.header.outerHeight();
        JT.Overlay.openClass = openClass;
        JT.Vars.bodyWrapper.css('padding-top',headerHeight);
        JT.Vars.body.addClass(JT.Overlay.openClass);
        JT.Overlay.isOpen = true;
    },
    queryFire: function(query, overlayQuery, overlayToFire, $linkList){
        // auto-fire and populate an overlay if deep linked
        // to page with a query string
        // EXAMPLE: /communities-gahanna.html?amenitiesOverlay=pet-friendly#amenities


        // from:
        //https://davidwalsh.name/query-string-javascript
        // because URLSearchParams doesn't work in IE

        function getUrlParameter(name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(query);
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        };

        var queryTerm = getUrlParameter(overlayQuery);

        if(queryTerm){
            // if there are additional overlay functions associated with an icon interaction,
            // e.g., amenities text, then click the icon to fire overlay with appropriate behavior

            // testing
            if($linkList){
                $linkList.each(function(){
                    var $this = $(this);
                    if(queryTerm === $this.attr('id')){
                        $this.trigger('click');
                    }
                });
            }else{
                //otherwise just fire the overlay
                JT.Overlay.open(overlayToFire);
            }
        }
    },
    openClass:''
};

JT.Scroll ={
    pageScroller: function($element, offset){
        var $this = $(this),
            scrollTo =  $element.offset().top;

        function scroller(scrollTo, offset){
            JT.Vars.htmlbody.removeClass('animateScroll')
                .stop(true,true)
                .addClass('animateScroll')
                .animate({
                        scrollTop: (scrollTo - offset)
                    }, 750, function(){JT.Vars.htmlbody.removeClass('animateScroll');}
                );
        }

        scroller(scrollTo,offset);
    }
};
