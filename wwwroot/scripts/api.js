define([], function () {
    const {
        protocol,
        hostname,
        port
    } = document.location;
    const base = `${protocol}//${hostname}:${port}`;

    /**
     * Super json fetch style
     * @param {RequestInfo} url 
     * @param {RequestInit} options - default : { method: "GET" }
     */
    function jfetch(url, options = {
        method: "GET"
    }, cb = e => e) {
        return fetch(url, options)
        .then(response => response.json())
        .then(response => cb(response))        
        .catch(error => console.error(error));
    }

    function getPostById(postId, cb) {
        let url = base + `/api/post/${postId}`
        return jfetch(url, null, cb)
    }

    function getRandomQuestion(cb) {
        let url = base + `/api/question/random`
        return jfetch(url, null, cb);
    }

    function getTagforPost(postId, cb) {
        let url = base + `/api/post/getTagsForPost?postId=${postId}`
        return jfetch(url, null, cb);
    }

    function getUserById(userId, cb) {
        let url = base + `/api/user/${userId}`
        return jfetch(url, null, cb);
    }

    function getCommentsForPost(postId, cb) {
        let url = base + `/api/comment/byPost/${postId}`
        return jfetch(url, null, cb);
    }

    function getUsersForComments(userIdArray, cb = () => null) {
        return Promise.all(userIdArray.map(e => getUserById(e, e => e)))
            .then(e => {
                cb(e);
                return e
            }).catch(e => console.error(e));
    }

    return {
        jfetch,
        getRandomQuestion,
        getTagforPost,
        getUserById,
        getCommentsForPost,
        getUsersForComments,
        getPostById,
    }
});