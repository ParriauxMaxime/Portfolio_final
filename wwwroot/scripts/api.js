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

    function getUsers(page = 0, pageSize = 50, cb) {
        const url = base + `/api/user?page=${page}&pageSize=${pageSize}`
        return jfetch(url, null, cb); 
    }

    function getQuestionsByScore(page = 0, pageSize = 50, cb) {
        const url = base + `/api/question?page=${page}&pageSize=${pageSize}&byScore=1`
        return jfetch(url, null, cb); 
    }

    function getPostsForUser(userId, cb, page=0, pageSize=50) {
        let url = base + `/api/user/post?userId=${userId}`
        return jfetch(url, null, cb);
    }

    function getCommentsForUser(userId, cb, page=0, pageSize=50) {
        let url = base + `/api/user/comment?userId=${userId}`
        return jfetch(url, null, cb);
    }

    function fetchServer(urlToFetch, cb) {
        let url = base + `/api/post/fetch?url=${encodeURI(urlToFetch)}`
        return fetch(url).then(response => {
                let str = '';
                const dec = new TextDecoder("utf-8");
                const reader = response.body.getReader();
                return reader.read().then(function t({
                    done,
                    value
                }) {
                    if (done) {
                        cb(str)
                        return str;
                    }
                    str += dec.decode(value)
                    reader.read().then(t);
                })
            })
            .catch(err => console.error(err));
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

    function getTags(page, pageSize, cb) {
        let url = base + `/api/tag?page=${page}&pageSize=${pageSize}`
        return jfetch(url, null, cb);        
    }

    function getSearchResults(page, pageSize, query, questionOnly = true, cb) {
        let url = base + `/api/post/searchInPosts?questionOnly=${+questionOnly}&query=${query}&page=${page}&pageSize=${pageSize}`
        return jfetch(url, null, cb);
    }

    function getPostsByIds(postIds, cb) {
        return Promise.all(postIds.map(e => getPostById(e)))
            .then(e => {
                cb(e);
                return e;
            }).catch(e => console.error(e));
    }

    function getSearchHistory(page, pageSize, cb) {
        let url = `/api/queryhistory?page${page}&pageSize${pageSize}`
        return jfetch(url, null, cb)
    }

    function addToSearchHistory(search, cb) {
        const url = '/api/queryhistory';
        let headers =  {"Content-Type": "application/json"}
        const body = JSON.stringify({id: 0, accountId: 1, query: search, creationDate: new Date(Date.now())});
        const options = {
            method: 'PUT',
            headers,
            body: '"'+body.replace(/\"/g, '\\"')+'"',
        }
        return (fetch(url, options, cb))
    }

    function getFavorites(page, pageSize, cb) {
        let url = `/api/history?page=${page}&pageSize=${pageSize}`   
        return jfetch(url, null, cb)
    }

    function findPostHistory(postId, cb) {
        let url = `/api/history/findPost?postId=${postId}`        
        return fetch(url, null)
        .then(response => response.status === 204 ? false : response.json())
        .then(r => cb(r))
        .catch(err => console.error(err));
    }

    function removeFromHistory(postId, cb) {
        let url = `/api/history/byPost/${postId}`
        let options = {
            method: 'DELETE',
        }
        return jfetch(url, options, cb)
    }

    function historyUpdate(entity, cb) {
        const url = `/api/history/`
        const body = JSON.stringify(entity)
        const options = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: '"'+body.replace(/\"/g, '\\"')+'"'            
        }
        return jfetch(url, options, cb);
    }
    function addToHistory(post, note = "", cb) {
        const url = '/api/history';
        let headers =  {"Content-Type": "application/json"}
        const body = JSON.stringify({id: 0, postId: post.id, accountId: 1, marked: true, note, creationDate: new Date(Date.now())});
        const options = {
            method: 'PUT',
            headers,
            body: '"'+body.replace(/\"/g, '\\"')+'"',
        }
        return jfetch(url, options, cb)
    }

    function getAnswersToPost(page, pageSize, postId, cb) {
        let url = base + `/api/post/parentId/${postId}?page=${page}&pageSize=${pageSize}`
        return fetch(url, null, cb).then(r => r.json()).then(r => cb(r)).catch(e => console.error(e));
    }

    function getWordCloud(query, cb) {
        let url = base + `/api/post/wordCloud?query=${query}`
        return jfetch(url, null, cb);
    }

    return {
        jfetch,
        addToSearchHistory,
        getRandomQuestion,
        getTagforPost,
        getUserById,
        getCommentsForPost,
        getUsersForComments,
        getQuestionsByScore,
        getSearchResults,
        getPostsByIds,
        getSearchHistory,
        getAnswersToPost,
        getPostById,
        fetchServer,
        getCommentsForUser,
        getPostsForUser,
        getWordCloud,
        getUsers,
        addToHistory,
        findPostHistory,
        getTags,
        historyUpdate,
        removeFromHistory,
        getFavorites
    }
});