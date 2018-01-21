define(['api', 'jquery', 'knockout'], function (api, $, ko) {
    function Search(props) {
        this.query = ko.observable("");
        this.searchResults = ko.observableArray([]);
        this.loading = ko.observable(true);
        this.haveResult = ko.computed(() => this.searchResults().length > 0 || this.loading())
        this.page = ko.observable(0);
        this.prev = ko.observable("");
        this.next = ko.observable("");
        this.pageSize = ko.observable(10);
        this.sizeAviable = ko.observableArray([10, 20, 50])
        this.totalResult = ko.observable(0);
        this.searchText = ko.computed(
            () => this.searchResults().length === 0 ?
            'Fetching results...' :
            `${this.totalResult()} Result${this.totalResult() != 1 ? 's' : ''}`)
      
        this.updateSearch = (query = this.query(), page = this.page(), pageSize = this.pageSize()) => {
            api.getSearchResults(page, pageSize, decodeURIComponent(query).replace(/\s+/g, ','), true, postIds => {
                this.next(postIds.next)
                this.totalResult(postIds.total)
                this.prev(postIds.prev)
                api.getPostsByIds(postIds.data.map(e => e.data.id), e => {
                    const hoc = e.map(e => ({...e, data: {...e.data, lightView: true}}))
                    this.searchResults(hoc);
                    this.loading(false);
                    return e;
                });
            });
        }

        const [hash, query] = location.hash.slice(1).split('/');
        
        if (query) {
            this.query(query);
            this.updateSearch();
        } else {
            location.assign('#Home');
        }

        this.goPrev = () => {
            if (this.prev() !== "") {
              this.page(this.page() - 1);
              this.loading(true)
              this.updateSearch();
            }
          }
      
          this.goNext = () => {
            if (this.next()) {
              this.page(this.page() + 1);
              this.loading(true)
              this.updateSearch();
            }
          }

          this.changePageSize = (d, e) => {
            this.pageSize(event.target.value);
            this.loading(true)
            this.updateSearch();
          }
    }

    return Search;
})
