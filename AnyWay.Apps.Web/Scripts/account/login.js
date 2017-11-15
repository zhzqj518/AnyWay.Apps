$(function () {
    var isLoggingin = false, loginText = "登&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;录", logginginText = "正在登录...";
    $("#user_name").val($.cookie("appUserName"));
    $("#user_pwd").val($.cookie("appUserPwd"));
    if ($.cookie("rememberme") == "true") {
        $(".tips label input")[0].checked = true;
    } else {
        $(".tips label input")[0].checked = false;
    }

    $(".btn-login").on("click", function () {
        if (isLoggingin) { return; }
        var user_name = $("#user_name").val();
        var user_pwd = $("#user_pwd").val();
        var verificate_code = $("#verificate_code").val();

        if (user_name.trim() == "") {
            $(".alert-error>span").html("用户名不能为空！")
            $(".alert-error").show();
            return;
        }

        if (user_pwd.trim() == "") {
            $(".alert-error>span").html("密码不能为空！");
            $(".alert-error").show();
            return;
        }

        if (verificate_code.trim() == "") {
            $(".alert-error>span").html("验证码不能为空！");
            $(".alert-error").show();
            return;
        }
        //alert(JSON.stringify({ user_name: user_name, user_pwd: user_pwd }));
        $(".alert-error").hide();
        $(".btn-login span").html(logginginText);
        isLoggingin = true;

        $.ajax({
            url: urlhelper.getRootPath() + "/Account/UserLogin",
            type: "post",
            data: { UserLoginName: user_name, UserPwd: $.md5(user_pwd), VerificateCode: verificate_code },
            success: function (text, flag, res) {
                isLoggingin = false;
                var msg = JSON.parse(arguments[2].responseText);
                if (msg.IsSuccess) {
                    //保存cookie，密码用md5加密后的存储
                    if ($(".tips label input")[0].checked) {
                        $.cookie("rememberme", "true", { expires: 7 });
                        $.cookie("appUserName", user_name, { expires: 7 });
                        $.cookie("appUserPwd", user_pwd, { expires: 7 });
                    } else {
                        $.cookie("rememberme", "false", { expires: -1 });
                        $.cookie("appUserName", user_name, { expires: -1 });
                        $.cookie("appUserPwd", user_pwd, { expires: -1 });
                    }
                    location.href = urlhelper.getRootPath() + "/Home/Main";
                } else {
                    $(".alert-error>span").html(msg.ErrorMsg);
                    $(".alert-error").show();
                    $("#validateCode").trigger("click");
                }
                $(".btn-login span").html(loginText);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                debugger;
                isLoggingin = false;
                $(".btn-login span").html(loginText);
                $(".alert-error>span").html("登录出现错误")
                $(".alert-error").show();
                $("#validateCode").trigger("click");
            }
        });
    });

    $("#user_name").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $(".btn-login").trigger("click");
        }
    });

    $("#user_pwd").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $(".btn-login").trigger("click");
        }
    });

    $("#verificate_code").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $(".btn-login").trigger("click");
        }
    });

    $("#validateCode").on("click", function () {
        $("#verificate_code").val("");
        this.src = urlhelper.getRootPath() + "/Account/GetValidateCode?time=" + (new Date()).getTime();
    });
});