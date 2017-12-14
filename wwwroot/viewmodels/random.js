define(['jquery', 'knockout'], function ($, ko) {
    const {
        protocol,
        hostname,
        port
    } = document.location;

    /**
     * Super json fetch style
     * @param {RequestInfo} url 
     * @param {RequestInit} options - default : { method: "GET" }
     */
    const jfetch = (
        url,
        options = {
            method: "GET"
        }
    ) => fetch(url, options).then(e => e.json())

    function getRandomQuestion(cb) {
        let url = `${protocol}//${hostname}:${port}`
        url += `/api/question/random`
        return jfetch(url)
            .then(response => cb(response))
            .catch(error => console.error(error))
    }

    function getTagforPost(postId, cb) {
        let url = `${protocol}//${hostname}:${port}`
        url += `/api/post/getTagsForPost?postId=${postId}`
        return jfetch(url)
            .then(response => cb(response))
            .catch(error => console.error(error))
    }

    function getUserById(userId, cb) {
        let url = `${protocol}//${hostname}:${port}`
        url += `/api/user/${userId}`
        return jfetch(url)
            .then(response => cb(response))
            .catch(error => console.error(error))
    }

    function getCommentsForPost(postId, cb) {
        let url = `${protocol}//${hostname}:${port}`
        url += `/api/comment/byPost/${postId}`
        return jfetch(url)
            .then(response => cb(response))
            .catch(error => console.error(error))
    }

    function Random(props) {
        this.post = ko.observable({})
        this.tags = ko.observableArray([]);
        this.user = ko.observable({})
        this.comments = ko.observableArray([]);
        this.updatePost = () => {
            getRandomQuestion((e) => {
                this.post(e)
                console.log('post =', e);
                getTagforPost(this.post().id, (e) => {
                    this.tags(e)
                    console.log('tags =', e);
                    getUserById(this.post().userId, e => {
                        this.user(e)
                        console.log('user =', e);
                        getCommentsForPost(this.post().id, e => {
                            this.comments(e);
                    //        const userMap = e.map(e => {
                        //        return getUserById(e.userId, (e => e))
                      //      })
                          //  console.log(userMap);
                            console.log('comments =', e);
                        })
                    })
                })
            })
        }
        this.updatePost();
    }

    return Random;
})