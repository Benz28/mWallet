{
            $(function () {
           $(document).on('click', '.open-login', function (e) {
               $('#msg').removeClass();
               $('#msg').addClass("login-msg").text('Please login below');
               $('#username').val('');
               $('#password').val('');
               $('.login-frame').fadeIn(500);
               $('.login-box').animate({ 'top': '50px' }, 500);
               e.preventDefault();
           });
           $(document).on('click', '.close-login', function (e) {
               $('.login-box').animate({ 'top': '-165px' }, 500);
               $('.login-frame').fadeOut(500);
               $('#username').val('');
               $('#password').val('');
               e.preventDefault();
           });
           $(document).on('click', '.login-btn', function (e) {
               var username = $('#username').val();
               var password = $('#password').val();
               var _loginMsg = $('#msg');
               if (username == '' || password == '') {
                   _loginMsg.addClass("error").removeClass("success");
                   _loginMsg.html("Fields should not be empty");
                   $('.login-box')
                            .animate({ left: -25 }, 20)
                            .animate({ left: 0 }, 60)
                            .animate({ left: 25 }, 20)
                            .animate({ left: 0 }, 60);
               } else {
                   var Objdata = {};
                   Objdata.username = username;
                   Objdata.password = password;
                   var url = "Login.aspx/CheckUser";
                   $.ajax({
                       type: "POST",
                       url: url,
                       data: JSON.stringify(Objdata),
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (response) {
                           if (response.d == true) {
                               _loginMsg.addClass("success").removeClass("error");
                               _loginMsg.html("Login was successful!");
                               $('.login-box').animate({ 'top': '-165px' }, 800);
                               $('.login-frame').fadeOut(500);
 
                           } else {
                               _loginMsg.addClass("error").removeClass("success");
                               _loginMsg.html("Invalid username & Password");
                               $('.login-box')
                                    .animate({ left: -25 }, 20)
                                    .animate({ left: 0 }, 60)
                                    .animate({ left: 25 }, 20)
                                    .animate({ left: 0 }, 60);
                           }
                       },
                       error: function (xmlHttpRequest, textStatus, errorThrown) {
                           alert('Error');
                       }
                   });
               }
              
               e.preventDefault();
           });
       });
}