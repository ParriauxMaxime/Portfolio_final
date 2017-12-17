define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function User(props) {
        this.user = ko.observable({})
        this.icon = ko.observable('');
        this.posts = ko.observableArray([]);
        this.comments = ko.observableArray([]);
        const sliceMe = (t, size = 100) => {
            const l = t.length;
            return l > size ? t.slice(0, size) + ' ...' : t;
        } 
        
        this.listPosts = ((link) => ({
            list: this.posts().map((e, i) => {
                return {
                    ...e,
                    text: ko.computed(() => {
                        let t;
                        if (e.title !== null) {
                            t = e.title;
                        }
                        else {
                            t = e.body;
                        }
                        return sliceMe(t);
                    })
                }
            }),
            title: 'Posts',
            link,
        }))
      
        this.listComments = ((link) => ({
            list: this.comments().map((e,i) => {
                return {
                    ...e,
                    text: ko.computed(() => sliceMe(e.text))
                }
            }),
            title: 'Comments',            
            link,
        }))
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
                api.getPostsForUser(id, p => {
                    this.posts(p);
                }); 
                api.getCommentsForUser(id, c => {
                    this.comments(c);
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
        const [hash, id]  = location.hash.slice(1).split('/')
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