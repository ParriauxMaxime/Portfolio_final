define(['api', 'jquery', 'jqcloud', 'knockout'], function (api, $, jQCloud, ko) {
  function WordCloud(props) {
    this.updateWordCloud = (query) => {
      // Convert http encoded query to sql query
      // e.g 'sql injection' becomes 'sql,injection'
      let sqlQuery = query.replace(/\s+/g, ',');

      api.getWordCloud(sqlQuery, wordArray => {
        console.log(wordArray);
        $('.wordcloud').jQCloud(wordArray, {
          autoResize: true
        });
      });
    };

    const [hash, query] = window.location.hash.slice(1).split('/');

    if (query) {
      this.updateWordCloud(query)
    } else {
      location.assign('#Home');
    }
  }

  return WordCloud;
});