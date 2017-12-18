define(['api', 'jquery', 'knockout'], function (api, $, ko) {
  function Question(props) {
    this.question = ko.observable({});
    this.loadingAnswers = ko.observable(true);
    this.answers = ko.observableArray([]);
    this.answersPage = ko.observable(0);
    this.answersPageSize = ko.observable(10);
    this.prev = ko.observable("");
    this.next = ko.observable("");
    this.totalAnswer = ko.observable(0);
    this.sizeAviable = ko.observableArray([10, 20, 50])

    this.answerText = ko.computed(
      () => this.answers().length === 0 ?
      'Fetching answer...' :
      `${this.totalAnswer()} Answer${this.totalAnswer() != 1 ? 's' : ''}`)

    this.getAnswers = (id, page = this.answersPage(), pageSize = this.answersPageSize()) => {
      api.getAnswersToPost(page, pageSize, id, answerIds => {
        console.log(answerIds);
        this.next(answerIds.next)
        this.totalAnswer(answerIds.total)
        this.prev(answerIds.prev)
        api.getPostsByIds(answerIds.data.map(e => e.data.id), e => {
          this.answers(e);
          this.loadingAnswers(false);
          return e;
        });
      });
    }
    this.updateQuestion = (id) => {
      let postId = id;
      api.getPostById(postId, e => {
        this.question(e.data);
        this.getAnswers(e.data.id)
      });
    };
    const [hash, id] = window.location.hash.slice(1).split('/');

    if (props.id) {
      this.updateQuestion(props.id)
    } else if (id) {
      this.updateQuestion(id)
    } else {
      location.assign('#Home');
    }

    this.goPrev = () => {
      if (this.prev() !== "") {
        this.answersPage(this.answersPage() - 1);
        this.loadingAnswers(true)
        this.getAnswers(this.question().id);
      }
    }

    this.goNext = () => {
      if (this.next()) {
        this.answersPage(this.answersPage() + 1);
        this.loadingAnswers(true)
        this.getAnswers(this.question().id);
      }
    }

    this.changePageSize = (d, event) => {
      this.answersPageSize(event.target.value);
      this.loadingAnswers(true)
      this.getAnswers(this.question().id);
    }
  }

  return Question;
})