define(['api', 'jquery', 'jqcloud', 'knockout'], function (api, $, jQCloud, ko) {
  function WordCloud(props) {
    this.updateWordCloud = (query) => {
      // Convert http encoded query to sql query
      // e.g 'sql injection' becomes 'sql,injection'
      let sqlQuery = query.replace(/\s+/g, ',');

      api.getWordCloud(sqlQuery, wordArray => {
        $('.wordcloud').jQCloud(wordArray, {
          autoResize: true
        });
      });
    };

    this.createWordCloud = (page, pageSize, cb) => {
      api.getTags(page, pageSize, (source) => {
        const mapped = source.data.map(e => {
          return {
            text: e.data.tagName,
            weight: 4 + Math.random() * 3,
            link: `#Search/${e.data.tagName}`
          }
        });
        cb(mapped)
      })
    }

    const [hash, query] = window.location.hash.slice(1).split('/');

    if (query) {
      this.updateWordCloud(query)
    } else {
      this.createWordCloud(0, 50, (e) => {
        $('.wordcloud').jQCloud(e, {
          autoResize: true,
          delay: 100,
        })
      })
      setInterval(() => {
        this.createWordCloud(Math.round(Math.random() * 37), 50, (e) => {
          $('.wordcloud').jQCloud('update', e)
        })
      }, 15000)
    }
  }
  return WordCloud;
});