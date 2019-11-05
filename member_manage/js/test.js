$('#myModal3').on('show.bs.modal', function () {
    $.ajax({
        url: "/tools/submit_ajax.ashx?action=getoncemoney",
        type: "get",
        async: false,
        success: function (result) {
            var obj = eval('(' + result + ')');
            var oncemoney = obj.oncemoney;
            var ptmoney = obj.ptrate;
            $("input[name='rate1']").val(ptmoney);
            $("input[name='rate2']").val(oncemoney);
        }
        
    })
})