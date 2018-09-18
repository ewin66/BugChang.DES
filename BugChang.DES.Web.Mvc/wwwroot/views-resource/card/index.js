﻿var CardIndex = function () {
    var table;

    $(function () {

        //toastr提示2s自动关闭
        window.toastr.options.timeOut = 2000;

        //初始化操作代码
        Common.initOperations('Card');

        //初始化页面元素
        initPageElement();

        //初始化table
        initTable();

        //初始化条码类型
        initUsers();

        //新增
        $('#CardCreateForm').submit(function (e) {
            e.preventDefault();
            var data = $(this).serialize();
            try {
                $.post('/Card/Create',
                    data,
                    function (result) {
                        if (result.success) {
                            resetForm();
                            //关闭模态
                            $('#CardCreateModal').modal('hide');
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

        $('table').delegate('.edit-card',
            'click',
            function () {
                var cardId = $(this).attr('data-card-id');
                editCard(cardId);
            });

        $('table').delegate('.delete-card',
            'click',
            function () {
                var cardId = $(this).attr('data-card-id');
                var cardNo = $(this).attr('data-card-no');
                deleteCard(cardId, cardNo);
            });
        $('table').delegate('.change-enabled',
            'click',
            function () {
                var cardId = $(this).attr('data-card-id');
                changeEnabled(cardId);

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
                url: '/Card/GetCards'
            },
            stateSave: true,
            columns: [
                {
                    data: 'id',
                    title: '主键'

                },
                {
                    data: 'userName',
                    title: '用户'
                },
                {
                    data: 'number',
                    title: '编号'
                },
                {
                    data: 'value',
                    title: '卡号'
                },
                {
                    data: 'enabled',
                    title: '启用状态'
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
                    targets: 4,
                    render: function (data, type, row) {
                        var strHtml = '';
                        if (Common.hasOperation('Card.Enabled')) {
                            if (row.enabled) {
                                strHtml += '<button class="btn btn-danger btn-xs change-enabled" data-card-id=' + row.id + '>禁用</button>';
                            } else {
                                strHtml += '<button class="btn btn-success btn-xs change-enabled" data-card-id=' + row.id + '>启用</button>';
                            }
                        } else {
                            if (row.enabled) {
                                strHtml = '<label class="label label-success">已启用</label>';
                            } else {
                                strHtml = '<label class="label label-danger">已禁用</label>';
                            }
                        }

                        return strHtml;
                    }
                },
                {
                    targets: 9,
                    render: function (data, type, row) {
                        var strHtml = '';
                        if (Common.hasOperation('Card.Edit')) {
                            strHtml += '<button class="btn btn-info btn-xs edit-card" data-card-id=' + row.id + '>修改</button>&nbsp;';
                        }
                        if (Common.hasOperation('Card.Delete')) {
                            strHtml += '<button class="btn btn-danger btn-xs delete-card" data-card-id=' + row.id + ' data-card-no=' + row.number + '>删除</button>';
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

    //初始化用户下拉列表
    function initUsers() {
        $.get('/Card/GetUsers',
            function (data) {
                $('.user-select').select2({
                    data: data,
                    allowClear: false
                });
            });
    }


    //编辑
    function editCard(id) {
        $('#CardEditModal .modal-content').load('/Card/EditCardModal/' + id);
        $('#CardEditModal').modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
    }

    //删除
    function deleteCard(cardId, cardName) {
        window.swal({
            title: '确定删除' + cardName + '?',
            //text: '删除后无法恢复数据!',
            icon: 'warning',
            buttons: ['取消', '确定'],
            dangerMode: true
        }).then((willDelete) => {
            if (willDelete) {
                $.post('/Card/Delete/' + cardId,
                    function (result) {
                        if (result.success) {
                            window.swal('操作成功', cardName + '已被删除!', 'success');
                            refresh();
                        } else {
                            window.swal('操作失败', result.message, 'error');
                        }
                    });
            }
        });
    }

    //启用更改
    function changeEnabled(id) {
        $.post("/Card/ChangeEnabled/" + id, function (result) {
            if (result.success) {
                //刷新页面
                refresh();
                window.toastr.success('操作成功');
            } else {
                window.toastr.error(result.message);
            }
        });
    }

    //清空表单
    function resetForm() {
        $('#CardCreateForm')[0].reset();
    }

    //刷新页面
    function refresh() {
        //刷新表格
        table.ajax.reload();
    }

    //重新加载页面
    function reload() {
        window.location.reload();
    }

    //初始化页面元素
    function initPageElement() {
        if (!Common.hasOperation('Card.Create')) {
            $('#btnAddCard').hide();
        }
    }

    //向外暴露方法
    return { refresh: refresh };
}();

