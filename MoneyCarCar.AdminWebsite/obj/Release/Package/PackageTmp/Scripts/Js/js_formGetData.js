//将表单数据转化为JSON数据对象
function getFormData(fromId, params) {
    var formArray = $("#" + fromId).serializeArray();
    var oRet = {};
    for (var i in formArray) {
        if (typeof (oRet[formArray[i].name]) == 'undefined') {
            if (params) {
                oRet[formArray[i].name] = (formArray[i].value == "true" || formArray[i].value == "false") ? formArray[i].value == "true" : formArray[i].value;
            }
            else {
                oRet[formArray[i].name] = formArray[i].value;
            }
        }
        else {
            if (params) {
                oRet[formArray[i].name] = (formArray[i].value == "true" || formArray[i].value == "false") ? formArray[i].value == "true" : formArray[i].value;
            }
            else {
                oRet[formArray[i].name] += "," + formArray[i].value;
            }
        }
    }
    return oRet;
}