define(['jquery', 'knockout'], function ($, ko) {
    const {
        protocol,
        hostname,
        port
    } = document.location;

    const logErrors = error => console.error(error);
    const pipeResult = callback => response => callback(response);

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

    // base url
    const base = `${protocol}//${hostname}:${port}`;

    function getSearchResults(query, cb) {
        const url = `${base}/api/post/searchInPosts?questionOnly=1&query=${encodeURIComponent(query)}`
        return jfetch(url)
            .then(pipeResult(cb))
            .catch(logErrors)
    }

    function getPostById(postId, cb) {
        const url = `${base}/api/post/${postId}`
        return jfetch(url)
            .then(pipeResult(cb))
            .catch(logErrors)
    }

    function getTagforPost(postId, cb) {
        const url = `${base}/api/post/getTagsForPost?postId=${postId}`
        return jfetch(url)
            .then(pipeResult(cb))
            .catch(logErrors)
    }

    function getUserById(userId, cb) {
        const url = `${base}/api/user/${userId}`
        return jfetch(url)
            .then(pipeResult(cb))
            .catch(logErrors)
    }

    function getSearchQuery() {
        const tokens = document.location.search.substring(1).split("=");
        const queryIdx = tokens.indexOf('query');

        if (queryIdx < 0) return null;
        if (queryIdx + 1 >= tokens.length) return null;

        return tokens[queryIdx + 1];
    }

    function Search(props) {
        this.results = ko.observableArray();
        this.updateSearch = () => {
            
            const query = getSearchQuery();

            if (query === null) return;

            getSearchResults(query, e => {
                console.log(e);
                e = [e[0]];
                for (let postId of e) {
                    let result = {};
                    getPostById(postId, post => {
                        result.post = post;
                        getTagforPost(postId, e => {
                            result.tags = e;
                            getUserById(post.data.userId, e => {
                                result.user = e;
                                this.results.push(result);
                                console.log(this.results());
                            });
                        })
                    });
                }
            });
        }
        this.updateSearch();

    }

    return Search;
})