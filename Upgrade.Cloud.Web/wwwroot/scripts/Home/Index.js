$(function () {
    var mainView = new MainInit();
    mainView.Init();
    
});

//主页面加载
var MainInit = function () {
    var oTableInit = new Object();
    var file = new FileInit(0);
    file.Init();


    var client = new ClientInit(0);
    client.Init();

    var bucket = new BucktInit();
    bucket.Init();

    var park = new ParkInit(0);
    park.Init();

    $("#btn_addBucket").click(function () {
        $("#bucketModal").find(".form-control").val("");
        $('#bucketModal').modal();
    });

    //table初始化
    oTableInit.Init = function () {
        $('#tb_upgradeItems').bootstrapTable({
            url: '/Home/GetUpgradeItems',
            method: 'get',
            toolbar: '#toolbar',
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "desc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: true,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            //height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'Name',
                title: '更新名称',
                editable: {
                    type: 'text',
                    title: '更新名称',
                    validate: function (value) {
                        if (!value) {
                            return "更新名称不能为空";
                        }
                    }
                }

            }, {

                field: 'IsValid',
                title: '是否有效',
                formatter: function (value, row, index) {
                    if (value == true)
                        return "有效";
                    else
                        return "无效";
                },
                sortable: true
            }, {
                field: 'CreateTime',
                title: '创建时间',
                sortable: true
            },
            {
                field: 'Content',
                title: '更新内容',
                editable: {
                    type: 'textarea',
                    title: '更新内容',
                    style: "display:-webkit-box;-webkit-line-clamp: 1;overflow: hidden;-webkit-line-break: auto;-webkit-box-orient: vertical;",
                    validate: function (value) {
                        if (!value) {
                            return "更新名称不能为空";
                        }
                    },
                    noeditFormatter: function (value, row, index) {
                        return "<a href=\"javascript: void (0)\" data-name=\"Content\" data-value=\"" + value + "\" class=\"editable editable-pre-wrapped editable-click\" style=\"display:-webkit-box;-webkit-line-clamp: 1;overflow: hidden;-webkit-line-break: auto;-webkit-box-orient: vertical;\"></a>";
                    },
                }
            }, {
                field: 'Id',
                title: '操作',
                width: 120,
                align: 'center',
                valign: 'middle',
                formatter: oTableInit.actionFormatter
            },],
            onEditableSave: function (field, row, oldValue, $el) {
                $.ajax({
                    type: 'POST',
                    url: '/Home/Edit',
                    data: row,
                    dataType: 'JSON',
                    success: function (data, textStatus, jqXHR) {
                        if (data.result == true) {
                            toastr.success("修改数据成功");
                        } else {
                            toastr.warning("修改数据失败，请查看后台日志")
                        }

                    },
                    error: function () { toastr.error("error"); }
                });
            },
            onLoadSuccess: function (aa, bb, cc) {
                //$("[data-toggle='tooltip']").tooltip();
            },

        });
    };
    
    oTableInit.actionFormatter = function (value, row, index) {
        var id = value;
        var result = "";
        result += "<a href='javascript:;' class='btn btn-xs green' onclick=\"ClientsViewById('" + id + "')\" title='查看客户端'><span class='glyphicon glyphicon-search'></span></a>";
        result += "<a href='javascript:;' class='btn btn-xs blue' onclick=\"UploadViewById('" + id + "')\" title='上传文件'><span class='glyphicon glyphicon-upload'></span></a>";
        result += "<a href='javascript:;' class='btn btn-xs blue' onclick=\"SetParkById('" + id + "')\" title='设置停车场'><span class='glyphicon glyphicon-user'></span></a>";
        if (row.IsValid == true) {
            result += "<a href='javascript:;' class='btn btn-xs red' onclick=\"SetValid('" + JSON.stringify(row).replace(/"/g, '&quot;') + "')\" title='置为无效'><span class='glyphicon glyphicon-star-empty'></span></a>";
        } else {
            result += "<a href='javascript:;' class='btn btn-xs red' onclick=\"SetValid('" + JSON.stringify(row).replace(/"/g,'&quot;') + "');\" title='置为有效'><span class='glyphicon glyphicon-star'></span></a>";
        }

        return result;
    };

    //置为有效无效按钮
    SetValid = function (row) {
        var obj = JSON.parse(row);
        obj.IsValid = !obj.IsValid;
        $.ajax({
            type: "POST",
            url: '/Home/Edit',
            data: obj,
            dataType: 'JSON',
            success: function (data, textStatus, jqXHR) {
                if (data.result == true) {
                    toastr.success("修改数据成功");
                    $("#tb_upgradeItems").bootstrapTable('refresh');
                } else {
                    toastr.error(data.msg);
                }

            },
            error: function () { toastr.error("error"); }
        });
    };
    SetParkById = function (Id) {
        park.itemId = Id;
        var params = {
            limit: 5,
            offset: 0,
            search: null,
            order: "desc",
            sort: null,
            itemId: Id
        };
        $("#tb_clientsets").bootstrapTable('refresh', { query: params });
        $('#parkModal').modal();
    };


    UploadViewById = function (Id) {
        file.itemId = Id;
        var params = {
            limit: 5,
            offset: 0,
            search: null,
            order: "desc",
            sort: null,
            itemId: Id
        };
        $("#tb_upgradefiles").bootstrapTable('refresh', { query: params });
        $("#fileModal").find(".form-control").val("");
        $('#fileModal').modal();
        $("#uploadfile").fileinput('reset');
        $("#bucketName").val("");
        $("#bucketName").selectpicker('refresh');;
    };
    ClientsViewById = function (Id) {
        file.itemId = Id;
        var params = {
            limit: 5,
            offset: 0,
            search: null,
            order: "desc",
            sort: null,
            itemId: Id
        };
        $("#tb_upgradeclients").bootstrapTable('refresh', { query: params });
        $('#clientModal').modal();

    };

    //table查询参数
    oTableInit.queryParams = function (params) {
        var temp = {
            limit: params.limit,
            offset: params.offset,
            search: params.search,
            order: params.order,
            sort: params.sort
        }
        return temp;
    };

    //初始化item新增页面
    var itemform = new ItemFormInit();
    itemform.Init();

    //绑定新增按钮
    $("#btn_add").click(function () {
        $("#myModalLabel").text("新增");
        $("#myModal").find(".form-control").val("");
        $('#myModal').modal();
    });

    return oTableInit;
};

//item加载
var ItemFormInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
           
        //退出表单后刷新验证
        //$("#myModal").on('hidden.bs.modal', function () {
        //    $("#itemform").data('bootstrapValidator').destroy();
        //    $('#itemform').data('bootstrapValidator', null);
        //    formValidator();
        //});

        //绑定验证
        //ItemFormInit.formValidator = function () {
        //    $("#itemform").bootstrapValidator({
        //        message: 'This value is not valid',
        //        feedbackIcons: {
        //            valid: 'glyphicon glyphicon-ok',
        //            invalid: 'glyphicon glyphicon-remove',
        //            validating: 'glyphicon glyphicon-refresh'
        //        },
        //        fields: {
        //            txt_name: {
        //                message: 'The name is not valid',
        //                validators: {
        //                    notEmpty: {
        //                        message: 'The name is required and can\'t be empty'
        //                    }
        //                }
        //            },
        //            txt_content: {
        //                message: 'The content is not valid',
        //                validators: {
        //                    notEmpty: {
        //                        message: 'The content is required and can\'t be empty'
        //                    }
        //                }
        //            }
        //        }
        //    });
        //};

        //保存绑定事件
        $("#btn_submit").click(function () {
            ////获取表单对象
            //var bootstrapValidator = $("#itemform").data('bootstrapValidator');
            ////手动触发验证
            //bootstrapValidator.validate();
            //if (bootstrapValidator.IsValid()) {
            postdata.Name = $("#txt_name").val();
            postdata.Content = $("#txt_content").val();
            $.ajax({
                type: "POST",
                url: "Home/AddItem",
                dataType: "json",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify(postdata),
                success: function (data, status) {
                    if (data.result == true) {
                        toastr.success(data.msg);
                        $("#tb_upgradeItems").bootstrapTable('refresh');
                    } else {
                        toastr.error(data.msg);
                    }
                },
                error: function () {
                    toastr.error('Error');
                }

            });
            //}
        });

    };

    return oInit;
};

//上传页面加载
var FileInit = function (Id) {
    var fInit = new Object();
    fInit.itemId = Id;
    fInit.Init = function () {

        $('#tb_upgradefiles').bootstrapTable({
            url: '/Home/GetUpgradeFiles',
            method: 'get',
            //toolbar: '#toolbar',
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "desc",                   //排序方式
            queryParams: fInit.fqueryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 5,                       //每页的记录行数（*）
            pageList: [5, 10, 15, 20],        //可供选择的每页的行数（*）
            search: true,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            //height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'BucketName',
                title: '存储空间名',
                sortable: true
            }, {
                field: 'FileName',
                title: '文件名',
                sortable: true,
                editable: {
                    type: 'text',
                    title: '文件名',
                    validate: function (value) {
                        if (!value) {
                            return "文件名不能为空";
                        }
                    }
                }

            }, {
                field: 'Key',
                title: '存储名',
                sortable: true
            }, {
                field: 'FilePath',
                title: '文件路径',
                sortable: true,
                editable: {
                    type: 'text',
                    title: '文件路径',
                    validate: function (value) {
                        if (!value) {
                            return "文件路径不能为空";
                        }
                    }
                }
            }, {
                field: 'CreateTime',
                title: '创建时间',
                sortable: true
            }, {
                field: 'Id',
                title: '操作',
                width: 50,
                align: 'center',
                valign: 'middle',
                formatter: fInit.actionFormatter
            },],
            onEditableSave: function (field, row, oldValue, $el) {
                $.ajax({
                    type: 'POST',
                    url: '/Home/EditFile',
                    data: row,
                    dataType: 'JSON',
                    success: function (data, textStatus, jqXHR) {
                        if (data.result == true) {
                            toastr.success("修改数据成功");
                        } else {
                            toastr.warning("修改数据失败，请查看后台日志")
                        }

                    },
                    error: function () { toastr.error("error"); }
                });
            },
            onLoadSuccess: function (aa, bb, cc) {
                //$("[data-toggle='tooltip']").tooltip();
            },

        });

        $("#uploadfile").fileinput({
            language: 'zh', //设置语言
            showUpload: true, //是否显示上传按钮
            showCaption: true,//是否显示标题
            browseClass: "btn btn-primary", //按钮样式   
            uploadUrl: "/Home/Upload",
            enctype: 'multipart/form-data',
            overwriteInitial: false,
            uploadAsync: true,
            initialPreviewAsData: true,
            slugCallback: function (filename) {
                fInit.formValidator();
                //获取表单对象
                var bootstrapValidator = $("#fileform").data('bootstrapValidator');
                //手动触发验证
                bootstrapValidator.validate();
                return filename;
            },
            uploadExtraData: function () {
                var data = {
                    Id: fInit.itemId,
                    filePath: $("#filepath").val(),
                    bucketName: $("#bucketName").val()
                };
                return data;
            }
        });

        fInit.formValidator();
        $(".selectpicker").selectpicker().on('shown.bs.select', function (e) {
            fInit.loadBucketNames();
        });

        $("#uploadfile").on('filecleared', function (event, data, msg) {
            toastr.warning("文件已删除干净")
        });

        $("#uploadfile").on("fileuploaded", function (event, data, previewId, index) {
            if (data.response.result) {
                toastr.success(data.response.msg);
                $("#tb_upgradefiles").bootstrapTable('refresh');
                return true;
            } else {
                if (data.response.msg == "" || data.response.msg == null) {
                    toastr.error("上传失败");
                } else {
                    toastr.error(data.response.msg)
                }
            }
        });

        $("#fileModal").on('hidden.bs.modal', function () {
            $("#fileform").data('bootstrapValidator').destroy();
            $('#fileform').data('bootstrapValidator', null);
            fInit.formValidator();
        });
    };

    fInit.fqueryParams = function (params) {
        var temp ={
            limit: params.limit,
            offset: params.offset,
            search: params.search,
            order: params.order,
            sort: params.sort,
            itemId: fInit.itemId
        }
        return temp;
    };

    fInit.actionFormatter = function (value, row, index) {
        var id = value;
        var result = "";
        result += "<a href='javascript:;' class='btn btn-xs green' onclick=\"DeleteFileById('" + id + "')\" title='删除'><span class='glyphicon glyphicon-remove'></span></a>";
        return result;
    };

    DeleteFileById = function (Id) {
        $.ajax({
            type: "POST",
            data: { Id: Id },
            url: "/Home/DelFileById",
            success: function (data) {
                if (data.result) {
                    $("#tb_upgradefiles").bootstrapTable('refresh');
                    toastr.success(data.msg);
                } else {
                    toastr.error(data.msg);
                }
            },
            error: function () { toastr.error("error"); }
        });
    }
    //绑定验证
    fInit.formValidator = function () {
        $("#fileform").bootstrapValidator({
            message: 'This value is not valid',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                filepath: {
                    message: 'The filepath is not valid',
                    validators: {
                        notEmpty: {
                            message: 'The filepath is required and can\'t be empty'
                        }
                    }
                },
                bucketName: {
                    message: "The bucketName is not valid",
                    validators: {
                        notEmpty: {
                            message: "The bucketName is required and can\'t be empty"
                        }
                    }
                }
            }
        });
    };

    fInit.loadBucketNames = function () {
        $.get('/Home/GetBucketNames', function (data) {
            if (data.result) {
                $('#bucketName').html('');
                $.each(data.data, function (index, item) {
                    $("#bucketName").append('<option>' + item + "</option>");
                });
                $(".selectpicker").selectpicker('refresh');
                $(".selectpicker").selectpicker('show');
            } else {
                toastr.error("请检查网络环境，无法连上阿里云");
            }
        });

    }

    return fInit;
};

var ClientInit = function (Id) {
    var cInit = new Object();
    cInit.itemId = Id;
    cInit.Init = function () {
        $('#tb_upgradeclients').bootstrapTable({
            url: '/Home/GetUpgradeClients',
            method: 'get',
            //toolbar: '#toolbar',
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "desc",                   //排序方式
            queryParams: cInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 5,                       //每页的记录行数（*）
            pageList: [5, 10, 15, 20],        //可供选择的每页的行数（*）
            search: true,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            //height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'ParkId',
                title: '停车场Id',
                sortable: true
            }, {
                    field: 'IsUpgradeSucess',
                    title: '是否成功',
                    formatter: function (value, row, index) {
                        if (value == true)
                            return "成功";
                        else
                            return "失败";
                    },
                    sortable: true

                }, {
                    field: 'UpgradeRespMsg',
                    title: '升级情况',
                    class: 'colStyle',
                    formatter: cInit.paramsMatter,
                    width:300
                }, {
                field: 'CreateTime',
                title: '升级时间',
                sortable: true
                }, {
                    field: 'Id',
                    title: '操作',
                    width: 40,
                    align: 'center',
                    valign: 'middle',
                    formatter: cInit.actionFormatter
                }, ],
            onLoadSuccess: function (aa, bb, cc) {
                //$("[data-toggle='tooltip']").tooltip();
            },

        });
    };
    cInit.actionFormatter = function (value, row, index) {
        var id = value;
        var result = "";
        result += "<a href='javascript:;' class='btn btn-xs green' onclick=\"DeleteFileById('" + id + "')\" title='删除'><span class='glyphicon glyphicon-remove'></span></a>";
        return result;
    };

    cInit.paramsMatter = function (value, row, index) {
        var values = row.UpgradeRespMsg;
        var span = document.createElement('span');
        span.setAttribute('title', values);
        span.innerHTML = row.UpgradeRespMsg;
        return span.outerHTML;

    };

    DeleteFileById = function (Id) {
        $.ajax({
            type: "POST",
            data: { Id: Id },
            url: "/Home/DelClientById",
            success: function (data) {
                if (data.result) {
                    $("#tb_upgradeclients").bootstrapTable('refresh');
                    toastr.success(data.msg);
                } else {
                    toastr.error(data.msg);
                }
            },
            error: function () { toastr.error("error"); }
        });
    }

    cInit.queryParams = function (params) {
        var temp = {
            limit: params.limit,
            offset: params.offset,
            search: params.search,
            order: params.order,
            sort: params.sort,
            itemId: cInit.itemId
        }
        return temp;
    };

    return cInit;
};

var BucktInit=function(){
    var bInit = new Object();
    bInit.Init = function () {
        $("#btn_submit_bucket").click(function () {
            $.ajax({
                type: "POST",
                url: "/Home/CreateBucket",
                data: { name: $("#nbucketName").val() },
                success: function (data) {
                    if (data.result) {
                        toastr.success(data.msg);
                    } else {
                        toastr.error(data.msg);
                    }
                },
                error: function (error) {
                    toastr.error(error);
                }
            });
        });
    };

    return bInit;
};

var ParkInit = function (Id) {
    var pInit = new Object();
    pInit.itemId = Id;
    pInit.Init = function () {
        $('#tb_parks').bootstrapTable({
            url: '/Home/GetParks',
            method: 'get',
            //toolbar: '#toolbar',
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "desc",                   //排序方式
            queryParams: pInit.park_queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 5,                       //每页的记录行数（*）
            pageList: [5, 10, 15, 20],        //可供选择的每页的行数（*）
            search: true,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            //height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'ParkId',
                title: '停车场Id',
                sortable: true
            }, {
                field: 'ParkName',
                title: '是否成功',
                sortable: true

            }, {
                field: 'Id',
                title: '操作',
                width: 40,
                align: 'center',
                valign: 'middle',
                formatter: pInit.park_actionFormatter
            },],
            onLoadSuccess: function (aa, bb, cc) {
                //$("[data-toggle='tooltip']").tooltip();
            },

        });

        $('#tb_clientsets').bootstrapTable({
            url: '/Home/GetClientSets',
            method: 'get',
            //toolbar: '#toolbar',
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: true,                     //是否启用排序
            sortOrder: "desc",                   //排序方式
            queryParams: pInit.park_queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 5,                       //每页的记录行数（*）
            pageList: [5, 10, 15, 20],        //可供选择的每页的行数（*）
            search: true,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            //height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'ParkId',
                title: '停车场Id',
                sortable: true
            }, {
                field: 'ParkName',
                title: '停车场名称',
                sortable: true

            }, {
                field: 'Id',
                title: '操作',
                width: 40,
                align: 'center',
                valign: 'middle',
                formatter: pInit.set_actionFormatter
            },],
            onLoadSuccess: function (aa, bb, cc) {
                //$("[data-toggle='tooltip']").tooltip();
            },

        });

    };

    pInit.park_actionFormatter = function (value, row, index) {
        var id = value;
        var result = "";
        result += "<a href='javascript:;' class='btn btn-xs green' onclick=\"AddClientSet('" + JSON.stringify(row).replace(/"/g, '&quot;') + "')\" title='添加'><span class='glyphicon glyphicon-arrow-down'></span></a>";
        return result;
    };

    pInit.set_actionFormatter = function (value, row, index) {
        var id = value;
        var result = "";
        result += "<a href='javascript:;' class='btn btn-xs green' onclick=\"DeleteClientSetById('" + id + "')\" title='删除'><span class='glyphicon glyphicon-remove'></span></a>";
        return result;
    };

    pInit.park_queryParams = function (params) {
        var temp = {
            limit: params.limit,
            offset: params.offset,
            search: params.search,
            order: params.order,
            sort: params.sort
        }
        return temp;
    };

    pInit.set_queryParams = function (params) {
        var temp = {
            limit: params.limit,
            offset: params.offset,
            search: params.search,
            order: params.order,
            sort: params.sort,
            itemId: pInit.itemId,
        }
        return temp;
    };

    AddClientSet = function (row) {
        var obj = JSON.parse(row);
        obj.ItemId = pInit.itemId;
        $.ajax({
            type: "POST",
            url: '/Home/AddClientSet',
            data: obj,
            dataType: 'JSON',
            success: function (data, textStatus, jqXHR) {
                if (data.result == true) {
                    toastr.success("添加成功");
                    var params = {
                        limit: 5,
                        offset: 0,
                        search: null,
                        order: "desc",
                        sort: null,
                        itemId: pInit.itemId
                    };
                    $("#tb_clientsets").bootstrapTable('refresh', { query: params });
                } else {
                    toastr.error(data.msg);
                }

            },
            error: function () { toastr.error("error"); }
        });
    };
    DeleteClientSetById = function (Id) {
        $.ajax({
            type: "POST",
            data: { Id: Id },
            url: "/Home/DeleteClientSetById",
            success: function (data) {
                if (data.result) {
                    var params = {
                        limit: 5,
                        offset: 0,
                        search: null,
                        order: "desc",
                        sort: null,
                        itemId: pInit.itemId
                    };
                    $("#tb_clientsets").bootstrapTable('refresh', { query: params });
                    toastr.success(data.msg);
                } else {
                    toastr.error(data.msg);
                }
            },
            error: function () { toastr.error("error"); }
        });
    }

    return pInit;
}