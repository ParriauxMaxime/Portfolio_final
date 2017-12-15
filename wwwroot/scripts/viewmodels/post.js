define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function Post(post) {
        console.log(post);
        this.post = ko.observable(post)
        this.tags = ko.observableArray([]);
        this.user = ko.observable({})
        this.comments = ko.observableArray([]);
        this.commentsShowed = ko.observableArray([]);
        this.condensedComment = ko.observable(true);
        this.changeComment = () => {
            this.condensedComment(!this.condensedComment()); 
            return false;
        }
        this.numberComments = ko.computed(() => this.comments().length)
        this.updatePost = () => {
            api.getTagforPost(this.post().id, (e) => {
                this.tags(e)
            })
            api.getUserById(this.post().userId, e => {
                this.user(e)
            })
            api.getCommentsForPost(this.post().id, e => {
                const userIdArray = e.map(e => e.userId);
                const t = (c) => c.map(e => ({
                    ...e,
                    displayText: ko.computed(() => { 
                        return e.text.length > 100 ? e.text.slice(0, 50) + ' ...' : e.text
                    }),
                }));
                this.comments(t(e));
                this.numberComments = ko.computed(() => this.comments.length)
                this.commentsShowed(t(e.slice(0, 3)));                
                api.getUsersForComments(userIdArray, u => {
                    const userMap = e.map((e, i) => ({
                        ...e,
                        user: u[i]
                    }))
                    this.comments(t(userMap));
                    this.commentsShowed(t(userMap.slice(0, 3)));
                })
            })

            const highlightCode = () => {
                $('code').each(function (i, block) {
                    hljs.highlightBlock(block);
                });
            }

            /**
             * ATTENTION REQUIRED
             * !rant
             * 
             * The following line is the ethical proof that jQuery 
             * and the whole MVVM design pattern is such a failure 
             * and a pain in the ass to the dev.
             */
            setTimeout(highlightCode, 200);

        }
        if (post && post.hasOwnProperty('id'))
            this.updatePost()
    }

    return Post;
})