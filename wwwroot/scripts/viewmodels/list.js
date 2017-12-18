define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function List(props) {
        this.pagination = ko.observable(props.pagination || false)
        this.list = ko.observableArray(props.list ? props.list.map(props.mapping) : [])
        this.title = ko.observable(props.title);
        this.prev = ko.observable(props.prev === "" ? false : true);
        this.next = ko.observable(props.next === "" ? false : true);
        this.link = ko.observable(props.link);
        this.page = ko.observable(props.page || 0);
        this.pageSize = ko.observable(props.pageSize)
        this.sizeAviable = ko.observableArray([10, 20, 50])
        this.goPrev = () => {
            if (this.prev())
                props.goPrev()
        }
        this.goNext = () => {
            if (this.next())
                props.goNext();
        }
        this.goTo = ({
            id,
            postId
        }) => {
            if (this.link() === 'Comment') {
                location.assign(`#Post/${postId}`)
            } else {
                location.assign(`#${this.link()}/${id}`)

            }
        }

        this.changePageSize = (p, event) => {
            this.pageSize(event.target.value);
            props.changePageSize(event.target.value);
            return false;
        }
    }

    return List;
})