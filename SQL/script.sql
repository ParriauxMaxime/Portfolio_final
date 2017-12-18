use stackoverflow_sample_universal;

DROP PROCEDURE IF EXISTS getQuestion;
Delimiter //;
CREATE PROCEDURE getQuestion(IN pagenb INT, IN pageSize INT)
BEGIN
 DECLARE offset INT DEFAULT pagenb*pageSize;
 SELECT distinct * 
 from l_posts p
 JOIN postTypeId pt on pt.id = p.postTypeId 
 where pt.type = "Question"
 ORDER BY p.id
 LIMIT pageSize
 OFFSET offset;
END //;
Delimiter ;

CALL getQuestion(0, 10);

DROP PROCEDURE IF EXISTS getTagsForPost;
Delimiter //;
CREATE PROCEDURE getTagsForPost(IN postId INT)
BEGIN
 SELECT distinct t.tag 
 FROM l_tags_posts tp
 JOIN tags t on tp.tagId = t.id
 where tp.postId = postId
 ORDER BY tp.id;
 END //;
Delimiter ;

CALL getTagsForPost(5404941);

ALTER TABLE l_comments ADD INDEX (postId);
ALTER TABLE l_posts ADD INDEX (postTypeId);
ALTER TABLE l_posts ADD INDEX (parentId);
ALTER TABLE l_comments ADD INDEX (userId);
ALTER TABLE l_posts ADD INDEX (userId);

ALTER TABLE history CHANGE Notes note TEXT;

INSERT INTO accounts (id, name, creationDate) values (0, "anonymous", NOW());