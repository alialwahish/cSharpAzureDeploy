CREATE DATABASE  IF NOT EXISTS `cSharpBelt` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `cSharpBelt`;
-- MySQL dump 10.13  Distrib 5.7.20, for Linux (x86_64)
--
-- Host: localhost    Database: cSharpBelt
-- ------------------------------------------------------
-- Server version	5.7.22-0ubuntu0.16.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `activities`
--

DROP TABLE IF EXISTS `activities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `activities` (
  `ActivitiesId` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(45) NOT NULL,
  `Time` datetime NOT NULL,
  `Date` datetime NOT NULL,
  `NumOfPrts` int(11) NOT NULL,
  `Duration` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `IntrstsId` int(11) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Creator` varchar(45) NOT NULL,
  `durType` varchar(45) NOT NULL,
  PRIMARY KEY (`ActivitiesId`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `activities`
--

LOCK TABLES `activities` WRITE;
/*!40000 ALTER TABLE `activities` DISABLE KEYS */;
INSERT INTO `activities` VALUES (17,'first act','2018-07-20 13:03:00','2018-07-20 00:00:00',0,2,4,NULL,'first act description','Ali','Hours'),(22,'second act','2018-07-20 10:01:00','2018-07-20 00:00:00',0,3,5,NULL,'second act description','Rose','Hours'),(27,'new activity','2018-07-20 14:22:00','2018-07-21 00:00:00',0,4,4,NULL,'over the hill','Ali','Hours'),(28,'second act','2018-07-20 15:22:00','2018-07-21 00:00:00',1,3,6,NULL,'asdasdasdasdasd','sai','Hours');
/*!40000 ALTER TABLE `activities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `intrsts`
--

DROP TABLE IF EXISTS `intrsts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `intrsts` (
  `IntrstsId` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `ActivitiesId` int(11) NOT NULL,
  PRIMARY KEY (`IntrstsId`)
) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `intrsts`
--

LOCK TABLES `intrsts` WRITE;
/*!40000 ALTER TABLE `intrsts` DISABLE KEYS */;
INSERT INTO `intrsts` VALUES (49,5,28);
/*!40000 ALTER TABLE `intrsts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `First_Name` varchar(45) NOT NULL,
  `Last_Name` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Confirm_Password` varchar(255) NOT NULL,
  `Created_At` datetime NOT NULL,
  `Updated_At` datetime NOT NULL,
  `ActivitiesId` int(11) DEFAULT NULL,
  `IntrstsId` int(11) DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (4,'Ali','Bayati','ali.sb.bayati@gmail.com','AQAAAAEAACcQAAAAEElZHzGI320DzZIh1Fnw3FykN7EwJubl3waHcOaCboD28GvGImjf0ZG/ImPpsRTcaw==','AQAAAAEAACcQAAAAECAwp8BKo7yYiEwZuHCVkfhengNBpluhMCMJZMN1/8aSAxyzuQIzX2jdipSO8ZiERA==','2018-07-20 09:32:42','2018-07-20 09:32:42',NULL,NULL),(5,'Rose','Shuhayeb','rose@gmail.com','AQAAAAEAACcQAAAAEDvb4cbkiRjU7Vnhi81bHRmuGcAsth7lws5UX4aXN4p1j74UKDyYp0oTUxtEJu9g/Q==','AQAAAAEAACcQAAAAED/tRI8tc5NRUdwVzPwObyyS/4KO/74tk3TNyEzc9j7tXPIl+L8ALzvVduSYlI0xEg==','2018-07-20 10:49:07','2018-07-20 10:49:07',NULL,NULL),(6,'sai','sss','sai@gmail.com','AQAAAAEAACcQAAAAEAL7kHMmfzJ2L43G/57qWvXl4PuorJpDQ14ibBOSrSQKzLXqHJqvZ2CBLUItyOMq4w==','AQAAAAEAACcQAAAAEKBgpg9S6weyqkbF+97Rm6aWFR1L8oTWZzbtvwoXUqsD/9jtm8NQMkVt2e+2yMez/Q==','2018-07-20 11:51:53','2018-07-20 11:51:53',NULL,NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-07-20 16:10:38
