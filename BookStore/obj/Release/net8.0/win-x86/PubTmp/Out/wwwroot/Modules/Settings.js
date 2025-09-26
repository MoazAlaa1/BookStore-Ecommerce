var ClsSettings = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:7267/api/Setting", {}, "json",
            function (data) {
                console.log(data);
                $("#lnkFacebook").attr("href", data.facebookLink);
                $("#lnkYoutube").attr("href", data.youtubeLink);
                $("#lnktwitter").attr("href", data.twitterLink);
                $("#lnkinstagram").attr("href", data.instgramLink);
                $("#Address").text(data.address);
                $("#Description").text(data.websiteDescription);
                $("#ConnectNumber").text(data.contactNumber);
                $("#websiteName").text(data.websiteName);
                $("#Logo").attr("src", "/Uploads/Settings/" + data.logo);
                $("#Logo2").attr("src", "/Uploads/Settings/" + data.logo);
            }, function () { });
    }
}

ClsSettings.GetAll();