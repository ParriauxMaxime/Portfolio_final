define(['api', 'jquery', 'knockout'], function (api, $, ko) {
  function Question(props) {
    this.question = ko.observable({});
    this.loadingAnswers = ko.observable(true);
    this.answers = ko.observableArray([]);
    this.answerText = ko.computed(
      () => this.answers().length === 0 ?
      'Fetching answer..' :
      `${this.answers().length} Answer${this.answers().length > 1 ? 's' : ''}`)

    this.updateQuestion = (id) => {
      let postId = id;
      api.getPostById(postId, e => {
        this.question(e.data);

        api.getAnswersToPost(e.data.id, answerIds => {
          api.getPostsByIds(answerIds.result, e => {
            this.answers(e);
            this.loadingAnswers(false);
            return e;
          });
        });
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
  }

  return Question;
})