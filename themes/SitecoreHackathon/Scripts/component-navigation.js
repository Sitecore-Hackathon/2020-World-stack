jQuery(document).ready(function($) {
    console.log("hello")
    $(".component.navigation").each(function(){
      var submenuLink = $(this).find("li.submenu");
  
      function openList(list) {
        list.addClass("active");
      }
  
      function close(list) {
        list.removeClass("active");
      }
  
      submenuLink.click(function(){
        console.log($(this).find("ul:first"))
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
          if (!$(e.target).hasClass(".clearfix.active") || !$(e.target).parent().hasClass(".submenu")){
            console.log("true");
            close($(".clearfix.active"));
          };
      });
  
    });
  });
  