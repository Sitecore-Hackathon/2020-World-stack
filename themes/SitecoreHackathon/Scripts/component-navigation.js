jQuery(document).ready(function($) {
    $(".component.navigation").each(function(){
      var submenuLink = $(this).find("li.submenu");
  
      function openList(list) {
        list.addClass("active");
      }
  
      function close(list) {
        list.removeClass("active");
      }
  
      submenuLink.click(function(){
        var list = $(this).find("ul:first");
        openList(list);
      });
  
      submenuLink.on("contextmenu",function(){
        return false;
      });
  
      submenuLink.click(function(e){
        e.preventDefault();
      });

      $(document).on("click", function(e) {
          if (!$(e.target).hasClass("active") && !$(e.target).closest("li").hasClass("submenu")){
            close($(".clearfix.active"));
          };
      });
  
    });
  });
  