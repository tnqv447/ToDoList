$(document).ready(function () {

    var old_title = $('#task-title').val();
    var old_describe = $('#task-description').val();
    var task_id = $('#task_id').val();
    // set up date picker
    $('.date-picker-format').datepicker({
        autoclose: true,
        format: 'dd/mm/yyyy',
        minDate: 0,
        startDate: new Date(),
        useCurrent: true
    });
    $('.date-picker').datepicker({
        autoclose: true,
        format: 'dd/mm/yyyy',
        useCurrent: true
    })
    $('.date').datepicker({
        autoclose: true,
        format: 'dd/mm/yyyy',
        userCurrent: true
    });
    $('#task-end-date').datepicker('option', 'minDate', 0);
    // add new task ajax
    $("#add_task").click(function () {
        $.ajax({
            url: $(this).attr("formaction"),
        }).done(function (msg) {
            $("#addContent").html(msg);
            $("#create-form").modal("show");
        })
    })
    // delete task
    $(document).on('click','.delete',function(){
        var taskId = $(this).attr('id');
        var pageIndex = $('#page_index').val();
        $.ajax({
            type:"POST",
            url: $('#delete_url').data('request-url'),
            data:{
                taskId:taskId,
                pageNumber: pageIndex
            },
            success: function(result){
                $('#partial').html(result);
                $('#text_success').text('Xóa thành công');
                $('#success').fadeIn();
            },
            complete: function(){
                setTimeout(function(){
                    $('#success').fadeOut();
                    $('#failed').fadeOut();
                },2000)
            },
            error: function(){
                $('#text_failed').text('Xóa thất bại');
                $('#failed'.fadeIn());
            }
        })
    });
    // set up data + function ajax post update task detail
    $('#task-end-date').change(function () {
        var date_selected = $(this).val();
        if(date_selected == null || date_selected == ''){
            alert("Hãy chọn ngày kết thúc");
        }
        else{
            var act = 'Day';
            updateTask(act, date_selected)
        }
        
    });
    
    $(document).on('change', '#task-registered-user', function () {
        var act = 'Registered';
        var value = $('#task-registered-user option:selected').val();
        updateTask(act, value)
    })
    $(document).on('keypress', '#task-title', function (e) {
        var action = 'Title';
        var value = $(this).val();
        if (e.which == 13) {
            if (value == old_title) {
                return;
            }
            else {
                if(value == "" || value == null){
                    $('#text_failed').text('Tiêu đề công việc trống');
                    $('#failed').fadeOut();
                    setTimeout(function(){
                        $('#failed').fadeIn();
                    },2000)
                    return;
                }
                else{
                    updateTask(action, value.trim());
                    old_title = value.trim();
                }
                
            }
            
        }
        
    });

    $(document).on('focusout', '#task-title', function (e) {
        var action = 'Title';
        var value = $(this).val();
        
        if (value == old_title) {
            return;
        }
        else{
            if(value == null || value == ""){
                $('#text_failed').text('Tiêu đề công việc trống');
                $('#failed').fadeOut();
                setTimeout(function(){
                    $('#failed').fadeIn();
                },2000)
                return;
            }
            else {
                updateTask(action, value.trim());
                old_title = value.trim();
            }
        }
        

    });
    $(document).on('focusout ', '#task-description', function (e) {
        var action = 'Describe';
        var value = $(this).val();
        if (value == old_describe) {
            return;
        }
        else {
            updateTask(action, value.trim());
            old_describe = value.trim();
        }

    });
    $(document).on('change', '#task-status', function () {
        var value = $('#task-status option:selected').val();
        var act = 'Status';
        updateTask(act, value);
    })
    $(document).on('change', '#task-scope', function () {
        var value = $('#task-scope option:selected').val();
        var act = 'Scope';
        updateTask(act, value);
    })
    $(document).on('click', '#addUser', function () {
        $('#addUser').prop('disabled', true);
        $(".remove_user").prop('disabled', true);
        var id = $('#task-joint-users option:selected').val();
        var act = 'Add Joint User';
        updateTask(act, id);
    });
    $(document).on('click', '.remove_user', function () {
        $('#addUser').prop('disabled', true);
        $(".remove_user").prop('disabled', true);
        var id = $(this).attr("id");
        var act = 'Remove Joint User';
        updateTask(act, id);

    });
    $(document).on('click', '#sendButton', function () {
        var act = 'Comment';
        var value = $('#userInput').val();
        if(value == null || value == ""){
            $('#text_failed').text('Hãy nhập bình luận');
            $('#failed').fadeIn();
            setTimeout(function(){
                $('#failed').fadeOut();
            },1000)
            return;
        }
        else{
            $('#userInput').val("");
            updateTask(act, value);
        }
        
    })
    
    function setAddJointUser(id, role, name) {
        $('#addUser').prop('disabled', false);
        $(".remove_user").prop('disabled', false);
        var empty_id = $('#table_emp tr:last').attr('id');
        if (empty_id == "row_empty") {
            $("#row_empty").remove();
        }
        var str = '<tr id="row_' + id + '"><td id="name_' + id + '">' + name +
            '</td><td id="role_' + id + '">' + role +
            '</td><td class="text-right"><a id="' + id + '" class="btn btn-icon btn-danger remove_user">' +
            '<span class="btn-inner--icon"><i class="ni ni-fat-remove"style="color:#ffffff"></i></span><span class="btn-inner--text"' +
            'style="color:#ffffff">Xóa</span>' +
            '</td></tr>';
        $('table> tbody:last').append(str);
        $('option:selected', '#task-joint-users').remove();
        if ($('#task-joint-users').has('option').length == 0) {
            $('#addUser').prop('disabled', true);
            $('#task-joint-users').prop('disabled', true);
        }
    }
    function setRemoveJointUser(id, name, role) {
        $('#addUser').prop('disabled', false);
        $(".remove_user").prop('disabled', false);
        $("<option>").val(id).text(name).appendTo("#task-joint-users optgroup[label='" + role + "']");
        $("#row_" + id).remove();
        if ($('#task-joint-users').has('option').length > 0) {
            $('#addUser').prop('disabled', false);
            $('#task-joint-users').prop('disabled', false);
        }
        var tbody = $("#table_emp tbody");
        if (tbody.children().length == 0) {
            tbody.html('<tr id="row_empty"><td colspan="3" class="text-center"><strong>Danh sách rỗng<strong></td></tr>');
        }
    }
    function updateTask(action, val) {

        $.ajax({
            type: 'POST',
            url: $("#get_url").data('request-url'),
            dataType: 'json',
            data: {
                action: action,
                id: task_id,
                val: val
            },
            success: function (data) {
                if(data.check.trim() == "failed"){
                    $('#failed').fadeIn();
                }
                else{
                    switch (action) {
                        case 'Title':
                            $('#text_title').text(data.title);
                            $('#task-title').val(data.title);
                            break;
                        case 'Describe':
                            $('#task-description').val(data.describe);
                            break;
                        case 'Day':
                            var d = new Date();
    
                            var month = d.getMonth() + 1;
                            var day = d.getDate();
    
                            var output = (day < 10 ? '0' : '') + day + '/' +
                                (month < 10 ? '0' : '') + month + '/' +
                                d.getFullYear();
                            if (new Date(output) <= new Date(data.date)) {
                                var temp = "Công việc đang trong thời gian tiến hành";
                                $("#check-task").text(temp);
                                $("#check-task").css("color", "green");
                            }
                            else {
                                var temp = "Công việc đã quá hạn";
                                $("#check-task").text(temp);
                                $("#check-task").css("color", "red");
                            }
                            break;
                        case 'Registered':
                            if (data.message == "yes") {
                                $("#row_" + data.NewUserId).remove();
                                if (tbody.children().length == 0) {
                                    tbody.html('<tr id="row_empty"><td colspan="3" class="text-center"><strong>Danh sách rỗng</strong></td></tr>');
                                }
                            }
                            $("<option>").val(data.OldUserId).text(data.OldName).appendTo("#task-joint-users optgroup[label='" + data.OldRole + "']");
                            $("#task-joint-users").find('option[value="' + data.NewUserId + '"]').remove();
                            if ($('#task-joint-users').has('option').length == 0) {
                                $('#addUser').prop('disabled', true);
                                $('#task-joint-users').prop('disabled', true);
                            }
                            break;
                        case 'Add Joint User':
                            var name = $('#task-joint-users option:selected').text();
                            var role = $('#task-joint-users option:selected').parent().attr('label');
                            setAddJointUser(data.id, role, name);
                            break;
                        case 'Remove Joint User':
                            var name = $("#name_" + val).text();
                            var role = $("#role_" + val).text();
                            setRemoveJointUser(val, name, role);
                            break;
                        case 'Comment':
                            var cmt = data.comment[data.comment.length - 1];
                            var date = new Date(cmt.postDate);
                            var month = date.getMonth() + 1;
                            var day = date.getDate();
                            var str_date = (day < 10 ? '0' : '') + day + '/' +
                                (month < 10 ? '0' : '') + month + '/' +
                                date.getFullYear()
                            var html = '<div>' +
                                '<span style="font-size:16px;font-weight:600">' + cmt.userPostName + '</span>' +
                                '<span style="padding-left:10px;font-size:13px">' + str_date +
                                '</span></div><p class="botMsg">' + cmt.content + '</p>' +
                                '<div class="clearfix"></div>';
                            $('#chats').append(html);
                            break;    
    
    
                    }
                    $('#success').fadeIn();
                }
                
            },
            complete: function () {
                setTimeout(function () {
                    $('#success').fadeOut();
                    $('#failed').fadeOut();
                }, 2000)

            },
            error: function (xhr) {
                alert(xhr);
                flag_add = false;
                flag_remove = false;
            }
        })
    };
    $('#navbar-search-main').submit(function(e){
        e.preventDefault();
    })
    // for search bar
    $(document).on('keypress','#search_user_name',function(e){
        if(e.which==13){
            
            var token = $('input[name="__RequestVerificationToken"]').val();
            var searchString = $(this).val();
            $.ajax({
                type: "post",
                url: $(this).data('request-url'),
                data: {
                    __RequestVerificationToken: token,
                    searchString: searchString.trim()               
                },
                dataType: "html",
                success: function (result) {
                    $("#partial").html(result);
                }
            });
        }
    });
    // paging
    $(document).on('click','.page-link',function(){
        var id = $(this).attr('id').slice(5);
        var searchString = $('#search_user_name').val();
        var url = $('#getUrl').data('request-url');
        Paging(id,searchString,url);
    })
    function Paging(pageNumber,searchString,url){
        $.ajax({
            type: 'GET',
            url: url,
            
            data:{
                pageNumber: pageNumber,
                searchString: searchString
            },
            dataType:'html',
            success: function(result){
                $("#partial").html(result);
            },
            error: function(){
                alert("Không truy cập được dữ liệu");
            }
        })
    }
    $(document).on('change','#action_search',function(){
        var value = $('#action_search option:selected').val();
        switch(value){
            case '0':
                $('#txt_search').prop('disabled',true);
                $('#date_search').prop('disabled',false);
                $('#action_log').prop('disabled',true);
                $('#task_id').prop('disabled',true);
                $('#user_id').prop('disabled',true);
                $('#user_name').prop('disabled',true);
                break;
            case '1':
                $('#txt_search').prop('disabled',false);
                $('#date_search').prop('disabled',true);
                $('#action_log').prop('disabled',true);
                $('#task_id').prop('disabled',true);
                $('#user_id').prop('disabled',true);
                $('#user_name').prop('disabled',true);
                break;
            case '2':
                $('#txt_search').prop('disabled',true);
                $('#date_search').prop('disabled',true);
                $('#action_log').prop('disabled',true);
                $('#task_id').prop('disabled',true);
                $('#user_id').prop('disabled',true);
                $('#user_name').prop('disabled',false);
                break;
            case '3':
                $('#txt_search').prop('disabled',true);
                $('#date_search').prop('disabled',true);
                $('#action_log').prop('disabled',true);
                $('#task_id').prop('disabled',false);
                $('#user_id').prop('disabled',true);
                $('#user_name').prop('disabled',true);
                break;
            case '4':
                $('#txt_search').prop('disabled',true);
                $('#date_search').prop('disabled',true);
                $('#action_log').prop('disabled',true);
                $('#task_id').prop('disabled',true);
                $('#user_id').prop('disabled',false);
                $('#user_name').prop('disabled',true);
                break;
            case '5':
                $('#txt_search').prop('disabled',true);
                $('#date_search').prop('disabled',true);
                $('#action_log').prop('disabled',false);
                $('#task_id').prop('disabled',true);
                $('#user_id').prop('disabled',true);
                $('#user_name').prop('disabled',true);
                break;
        }
        
    });
    
    $(document).on('click','#send_filter',function(){
        var search_action = $('#action_search option:selected').val();
        var url = $(this).data('request-url');
        switch(search_action){
            case '0':
                var act = 'DateExec'
                var value = $('#date_search').val();
                SendFilter(act,value,url);
                break;
            case '1':
                var act = 'TaskName'
                var value = $('#txt_search').val().trim();
                SendFilter(act,value,url);
                break;
            case '2':
                var act = 'UserName'
                var value = $('#user_name option:selected').val();
                SendFilter(act,value,url);
                break;
            case '3':
                var act = 'TaskId'
                var value = $('#task_id option:selected').val();
                SendFilter(act,value,url);
                break;
            case '4':
                var act = 'UserId'
                var value = $('#user_id option:selected').val();
                SendFilter(act,value,url);
                break;
            case '5':
                var act = 'Action'
                var value = $('#action_log option:selected').val();
                SendFilter(act,value,url);
                break;
        }
    })
    function SendFilter(action,value,url){
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            type: "POST",
            url: url,
            
            data:
            {
                __RequestVerificationToken: token,
                value: value,
                action: action
            },
            dataType:"html",
            success: function(result){
                $('#partial').html(result)
            }
        });
    }
});