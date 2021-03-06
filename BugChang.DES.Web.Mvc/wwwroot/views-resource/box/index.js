﻿var BoxIndex = function () {
    var table;

    $(function () {

        //toastr提示2s自动关闭
        window.toastr.options.timeOut = 2000;

        //初始化操作代码
        Common.initOperations('Box');

        //初始化页面元素
        initPageElement();

        //初始化table
        initTable();

        //初始化交换场所
        initPlace();

        //新增箱格表单提交
        $('#BoxCreateForm').submit(function (e) {
            e.preventDefault();
            var data = $(this).serialize();
            try {
                $.post('/Box/Create',
                    data,
                    function (result) {
                        if (result.success) {
                            resetForm();
                            //关闭模态
                            $('#BoxCreateModal').modal('hide');
                            //刷新页面
                            refresh();
                            window.toastr.success('操作成功');
                        } else {
                            window.toastr.error(result.message);
                        }
                    });
            } catch (e) {
                console.log(e);
            }

        });

        $('table').delegate('.edit-box',
            'click',
            function () {
                var boxId = $(this).attr('data-box-id');
                editBox(boxId);
            });

        $('table').delegate('.delete-box',
            'click',
            function () {
                var boxId = $(this).attr('data-box-id');
                var boxName = $(this).attr('data-box-name');
                deleteBox(boxId, boxName);
            });

        $('table').delegate('.assign-object',
            'click',
            function () {
                var boxId = $(this).attr('data-box-id');
                assignObject(boxId);
            });



        $('#btnRefresh').click(function () {
            reload();
        });
    });

    //初始化table
    function initTable() {
        table = $('#table').DataTable({
            ordering: false,
            processing: true,
            serverSide: true,
            autoWith: true,
            ajax: {
                url: '/Box/GetBoxs'
            },
            stateSave: true,
            columns: [
                {
                    data: 'id',
                    title: '主键'

                },
                {
                    data: 'name',
                    title: '名称'
                },
                {
                    data: 'frontBn',
                    title: '正面BN号'
                },
                {
                    data: 'backBn',
                    title: '背面BN号'
                },
                {
                    data: 'permanentMessage',
                    title: '常驻提示信息'
                },
                {
                    data: 'placeName',
                    title: '交换场所'
                },
                {
                    data: 'enabled',
                    title: '状态'
                },
                {
                    data: 'createUserName',
                    title: '创建人'
                },
                {
                    data: 'createTime',
                    title: '创建时间'
                },
                {
                    data: 'updateUserName',
                    title: '最后更改人'
                },
                {
                    data: 'updateTime',
                    title: '最后更改时间'
                },
                {
                    data: null,
                    title: '操作'
                }
            ],
            columnDefs: [
                {
                    targets: 6,
                    render: function (data, type, row) {
                        var strHtml = '';
                        if (row.enabled) {
                            strHtml = '<label class="label label-success">已启用</label>&nbsp;';
                        } else {
                            strHtml = '<label class="label label-danger">已禁用</label>&nbsp;';
                        }
                        return strHtml;
                    }
                },
                {
                    targets: 11,
                    render: function (data, type, row) {
                        var strHtml = '';
                        if (Common.hasOperation('Box.AssignObject')) {
                            strHtml += '<button class="btn btn-primary btn-xs assign-object" data-box-id=' + row.id + '>分配流转对象</button>&nbsp;';
                        }
                        if (Common.hasOperation('Box.Edit')) {
                            strHtml += '<button class="btn btn-info btn-xs edit-box" data-box-id=' + row.id + '>修改</button>&nbsp;';
                        }
                        if (Common.hasOperation('Box.Delete')) {
                            strHtml += '<button class="btn btn-danger btn-xs delete-box" data-box-id=' + row.id + ' data-box-name=' + row.name + '>删除</button>';
                        }

                        return strHtml;
                    }
                }
            ],
            language: {
                url: '../../lib/datatables/language/chinese.json'
            }
        });
    }

    //初始化交换场所
    function initPlace() {
        $.get('/Box/GetPlaces',
            function (data) {
                $('.place-select').select2({
                    data: data,
                    allowClear: false
                });
            });
    }

    //分配流转对象
    function assignObject(id) {
        $('#AssignObjectModal .modal-content').load('/Box/AssignObjectModal/' + id);
        $('#AssignObjectModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
    }

    //编辑箱格信息
    function editBox(id) {
        $('#BoxEditModal .modal-content').load('/Box/EditBoxModal/' + id);
        $('#BoxEditModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
    }

    //删除箱格
    function deleteBox(boxId, boxName) {
        window.swal({
            title: '确定删除' + boxName + '?',
            //text: '删除后无法恢复数据!',
            icon: 'warning',
            buttons: ['取消', '确定'],
            dangerMode: true
        }).then((willDelete) => {
            if (willDelete) {
                var token = $("input[name='BugChangFieldName']").val();//隐藏域的名称要改
                $.ajax({
                    type: 'POST',
                    async: false,
                    cache: false,
                    data: null,
                    headers:
                        {
                            "BugChang-CSRF-HEADER": token //注意header要修改
                        },
                    url: "/Box/Delete/" + boxId,
                    success: function (result) {
                        if (result.success) {
                            window.swal('操作成功', boxName + '已被删除!', 'success');
                            refresh();
                        } else {
                            window.swal('操作失败', result.message, 'error');
                        }
                    }
                });
            }
        });
    }

    //清空表单
    function resetForm() {
        $('#BoxCreateForm')[0].reset();
    }

    //刷新页面
    function refresh() {
        //刷新表格
        table.draw(false);
    }

    //重新加载页面
    function reload() {
        window.location.reload();
    }

    //初始化页面元素
    function initPageElement() {
        if (!Common.hasOperation('Box.Create')) {
            $('#btnAddBox').hide();
        }
    }

    //向外暴露方法
    return { refresh: refresh };
}();

