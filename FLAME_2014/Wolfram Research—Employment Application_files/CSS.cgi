
body { margin: 0; padding: 0; }
a { outline: none; } 

#menu { width: 100%; height: 55px; background: #000 url('/common/images2008/headerBackground.gif') repeat-x 0 -2px; }
  #menu .logo { width: 153px; background: url('/common/images2010/wolframlogo10-blue.gif') no-repeat 20px 9px; }
    #menu .logo img { margin-left: 20px; }
  #menu .globalSearch { width: 180px; text-align: right; }
  #menu table { width: 960px;
	/*  FF */ margin: 0 0 0 0 !important; padding: 10px 0 0 0 !important;
	/* IE6 */ margin: 10px 0 0 0; padding: 0 0 0 0; }
	/* IE7 */ * + html #menu table { margin: 10px 0 0 0 !important; padding: 0 0 0 0 !important; }
  #menu td { padding-top: 8px; vertical-align: middle !important; }
  #menu td#menuContainer { padding: 8px 10px 0 25px; white-space: nowrap; }


/* * * * * * * menu * * * * * * */
.WRImenuWrap { display: inline; padding: 0; margin: 0; border: 0; font-family: Arial, Helvetica, Geneva, Verdana, serif; font-size: 11px; line-height: 23px; }
.WRImenuContents { display: none; position: absolute; left: -2000px; top: -2000px; z-index: 99997; padding: 0; margin: 0; border: none; white-space: nowrap; background: #6a6a6a; }
  .WRImenuContents .WRImenuContents { background: #808080; }

.WRImenuWrap a, .WRImenuWrap a:hover { outline: none; text-decoration: none; color: #fff !important; padding: 0 6px; }
  .WRImenuWrap a.topLevel { display: inline-block; margin-right: 5px; }
  .WRImenuWrap .WRImenuWrap a.WRImenuLink { background-image: url('/common/images/submenuarrow.gif'); background-repeat: no-repeat; background-position: right center; }
	@media screen and (-webkit-min-device-pixel-ratio:0) {
		.WRImenuWrap .WRImenuWrap a.WRImenuLink { margin-right: -3px; }
	}
  .WRImenuWrap a.itemborder, .WRImenuWrap a.menuItemBorder { border-bottom: 1px solid #999; }
  /* widths */
  .WRImenuContents a { display: inline-block; width: 195px; }
    #solutionsWide a { width: 245px; } /* #menuSubSolutions a */
      #solutionsWide .WRImenuContents a { width: 165px; }
      #solutionsWide #engineWide a { width: 180px; } /* #menuSubSolutions #menuSubEngineering a */
      #solutionsWide #financeWide a { width: 190px; } /* #menuSubSolutions #menuSubFinance a */
      #solutionsWide #designWide a { width: 240px; } /* #menuSubSolutions #menuSubDesign a */
      #solutionsWide #educationWide a { width: 215px; } /* #menuSubSolutions #menuSubEducation a */
      #solutionsWide #techWide a { width: 240px; } /* #menuSubSolutions #menuSubTechnology a */

  /* hovering */
  .WRIdropMenu a.WRImenuHover, a.topLevel:hover { background-color: #383838; color: #7787ff !important; }
  .WRImenuContents a.WRImenuHover, .WRImenuContents a:hover { background-color: #7787ff; color: #fff !important; }
/* * * * * * * end of menu * * * * * * */


#menuspace { margin-top: 22px; }

#menu form.headerSearchBox { margin: 1px 0 0 0; padding: 0; }
#menu .searchboxsub, #menu .searchboxsub-on { width: 155px; height: 16px; margin: 0 0 2px 0; padding: 1px 0 0 3px; border: 1px solid #666; font: 11px Arial, Helvetica, Verdana, sans-serif; }
	/* ie and safari hacks */
	html*#menu .searchboxsub, html*#menu .searchboxsub-on {
	height: 14px; padding-top: 0; margin-bottom: -1px;
	]height: 16px;
	]padding-top: 1px;
	]margin-bottom: 2px;
	}/*end*/
	.dummyend[id]{clear:both;}

	/*\*/
	* html #menu .searchboxsub, * html #menu .searchboxsub-on {
	height: 16px; padding-top: 1px; margin-bottom: 2px;
	}
	/*end*/
#menu .searchboxsub    { color: #bbb; background: #555; }
#menu .searchboxsub-on { color: #000; background: #fff; }
#menu .headerSearchSubmit { position: relative; padding: 0;
	/*  FF */ margin: 0 2px 1px -11px !important;
	/* IE6 */ margin: 0 2px 3px -11px; }
	/* IE7 */ * + html #menu .headerSearchSubmit { margin: 0 2px 3px -11px !important; }


/* * * * * * * m8 badge * * * * * * */
div#m8badge { position: absolute; top: 55px; left: 0; z-index: 100; width: 965px; height: 30px; text-align: right; }
#badgemenuspace { margin-top: 40px; }




/* * * * * * * new global footer stuff * * * * * */

table#globalFooter td { font:10px/17px Helvetica,Arial,Verdana,Geneva,sans-serif; color:#666; vertical-align: top;}
	table#globalFooter td img.vertline {vertical-align: middle; }
div#footerAboutLinks { margin: 0px 12px 0 0; }
  div#footerAboutLinks a { margin:0 5px; color:#666; }
    div#footerAboutLinks a:hover { color: #d86c00; }
    div#footerAboutLinks img { vertical-align:middle; }

table#globalFooter td img.negoLink { vertical-align: middle; margin: 2px 10px 0 0; }

div#NewsSocialWrapper { overflow:hidden; width:210px; }
div#footerSocialIcons { float:left; width: 95px; height:16px; text-align: right; }
  div#footerSocialIcons a { display: inline-block; width: 15px; height: 16px; margin: 1px 5px 0 0; vertical-align: middle; background: url('/common/images2010/socialIcons_15.png') no-repeat; }

/* hovering version */
	#footerSocialIcons a#rss { background-position: -48px -16px; }
    #footerSocialIcons a#linkedin { background-position: 0 -16px; }
    #footerSocialIcons a#facebook { background-position: -16px -16px; }
    #footerSocialIcons a#twitter { background-position: -32px -16px; }
    #footerSocialIcons a#youtube { background-position: -128px -16px; }
      #footerSocialIcons a:hover#rss { background-position: -48px 0; }
      #footerSocialIcons a:hover#linkedin { background-position: 0 0; }
      #footerSocialIcons a:hover#facebook { background-position: -16px 0; }
      #footerSocialIcons a:hover#twitter { background-position: -32px 0; }
      #footerSocialIcons a:hover#youtube { background-position: -128px 0; }


div#footerNewsletter { float:left; width: 78px; height:17px; text-align: right; }
div#footerNewsletter a { display: inline-block; width: 78px; height: 16px; margin: 1px 5px 0 0; vertical-align: middle; background: url('/common/images2010/newsletterBG.gif') no-repeat; }
	div#footerNewsletter a, div#footerNewsletter a:link, div#footerNewsletter a:visited { color:#666; }
	div#footerNewsletter a:hover, div#footerNewsletter a:hover:visited { color:#bb0000; }
	div#footerNewsletter a#newsletter { background-position: 0 0; }


