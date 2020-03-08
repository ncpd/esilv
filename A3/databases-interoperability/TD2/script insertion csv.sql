LOAD DATA LOCAL INFILE '/temp/locationComplements.csv'
  INTO TABLE `location`
  FIELDS TERMINATED BY ';'
  LINES TERMINATED BY '\n';