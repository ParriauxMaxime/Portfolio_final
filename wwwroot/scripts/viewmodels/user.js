define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function User(props) {
        this.user = ko.observable({})
        this.icon = ko.observable('');
        this.getUser = (id) => {
            api.getUserById(id, (e) => {
                this.user({
                    ...e,
                    data: {
                        ...e.data,
                        age: ko.computed(() => e.data.age !== null ? e.data.age : 'N/A'),
                        location: ko.computed(() => e.data.location !== null ? e.data.location : 'N/A')
                    }
                });
                api.fetchServer(`https://stackoverflow.com/users/${e.data.id}/${e.data.displayName}`, (e) => {
                    const html = $.parseHTML(e);
                    $.each(html, (i, el) => {
                        if ($(el).hasClass('container')) {
                            const im = $(el).find('img.avatar-user');
                            this.icon(im[0].src)
                        }
                    })
                })
            })
        }
        const [hash, id] = location.hash.slice(1).split('/')
        if (props.id) {
            this.getUser(props.id)
        } else if (id) {
            this.getUser(id);
        } else {
            location.assign('#Home');
        }
    }

    return User;
})