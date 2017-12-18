define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function User(props) {
        this.user = ko.observable({})
        this.icon = ko.observable('');
        this.posts = ko.observableArray([]);
        this.comments = ko.observableArray([]);
        this.commentPage = ko.observable(0);
        this.commentPageSize = ko.observable(10);
        this.postPage = ko.observable(0);
        this.postPageSize = ko.observable(10);
        const sliceMe = (t, size = 100) => {
            const l = t.length;
            return l > size ? t.slice(0, size) + ' ...' : t;
        } 

        const canPrev = (page) => {
            if (page !== 0) 
                return true;
            return false;
        }

        const canNext = (page, pageSize, length) => {
            if ((page + 1) * pageSize < length)
                return true;
            return false;
        }
        
        this.listPosts = ((link) => ({
            mapping: (e, i) => {
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
            },
            list: this.posts(),
            title: 'Posts',
            link,
        }))
      
        this.listComments = ((link) => ({
            mapping: (e,i) => {
                return {
                    ...e,
                    text: ko.computed(() => sliceMe(e.text))
                }
            },
            list: this.comments(),
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
                }, this.postPage, this.postPageSize); 
                api.getCommentsForUser(id, c => {
                    this.comments(c);
                }, this.commentPage(), this.commentPageSize());
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