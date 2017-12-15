const MAX_BODY_LENGTH = 500;

define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function Post(post) {

        // Provide a lighter body in search view
        if (post.lightView) {
            // Clean out HTML code first
            post.lightBody = post.body.replace(/<[^>]*>/g, '');
            if (post.lightBody.length > MAX_BODY_LENGTH + 3)
                post.lightBody = post.lightBody.slice(0, MAX_BODY_LENGTH) + '...';

            // Highlight matches with surrounding characters
            // Can lead to false-positives
            let query = api.getSearchQuery();
            for (let word of query.split(/\s/)) {
                post.lightBody = post.lightBody.replace(new RegExp(`(\\w*${word}\\w*)`, 'ig'), '<b>$1</b>');
            }
        }

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

            // Don't load comments in search view
            if (!this.lightView) {
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
            }

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

        // Gets Post object from post.js
        this.goToQuestion = (post) => {
            let postId = post.post().id; // this is the Post() object, it has a post property
            document.location.assign(`#Question/postId`);
        }
    }

    return Post;
})