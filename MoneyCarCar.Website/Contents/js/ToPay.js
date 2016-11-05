function ToPayForm(data) {
    var postUrl = data.ErrorMsg;
    var postData = data.Tag;
    var f = document.createElement("form");
    document.body.appendChild(f);
    f.method = "post";
    f.action = postUrl;
    var i = document.createElement("input");
    f.appendChild(i);
    i.type = "hidden";
    i.name = "req";
    i.value = postData.req;
    var i1 = document.createElement("input");
    f.appendChild(i1);
    i1.type = "hidden";
    i1.name = "sign";
    i1.value = postData.sign;
    f.submit();
}