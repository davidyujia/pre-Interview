var vue;

function dateFormat(val) {
    if (!val) {
        return '';
    }
    val = new Date(val);
    var month = val.getMonth() + 1;
    if (month < 10) {
        month = '0' + month;
    }
    var d = val.getDate();
    if (d < 10) {
        d = '0' + d;
    }

    return month + '/' + d + '/' + val.getFullYear();
};
axios.post('signin').then(function (json) { //get token first
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + json.data;
    vue = new Vue({
        el: '#app',
        components: {
            vuejsDatepicker
        },
        data: {
            employees: [],
            editMode: false,
            editForm: {},
            employee: {}
        },
        filters: {
            fullName: function (val) {
                let firstName = val.firstName ? val.firstName : '';
                let lastName = val.lastName ? val.lastName : '';
                return firstName + " " + lastName;
            },
            dateFormat: dateFormat,
        },
        methods: {
            openTasks: function (item) {
                let root = this;
                root.employee = item;
                $('#employeeDetail').modal('show');
            },
            add: function () {
                this.editMode = false;
                this.editForm = {
                    tasks: []
                };
                $('#employeeEdit').modal('show');
            },
            edit: function (item) {
                this.editMode = true;
                this.editForm = JSON.parse(JSON.stringify(item));
                $('#employeeEdit').modal('show');
            },
            addTask: function () {
                if (!this.editForm.tasks) {
                    this.editForm.tasks = [];
                }
                this.editForm.tasks.push({});
            },
            removeTask: function (item) {
                this.editForm.tasks = this.editForm.tasks.filter(function (x) {
                    return x != item;
                });
            },
            submit() {
                let root = this;
                let func = root.editMode ? axios.put : axios.post
                func('Employees', root.editForm).then(function (json) {
                    root.load();
                    $('#employeeEdit').modal('hide');
                });
            },
            load: function () {
                let root = this;
                axios.get('Employees').then(function (json) {
                    console.log(json);
                    root.employees = json.data;
                });
            }
        },
        mounted: function () {
            this.load();
        }
    });
});