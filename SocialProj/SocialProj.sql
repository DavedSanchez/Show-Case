-- MySQL dump 10.13  Distrib 5.6.24, for Win32 (x86)
--
-- Host: 127.0.0.1    Database: trialdb
-- ------------------------------------------------------
-- Server version	5.6.26-log

CREATE TABLE `trialdb.user` (
  `idUser` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(45) NOT NULL,
  `LastName` varchar(45) NOT NULL,
  `Email` varchar(45) DEFAULT NULL,
  `UserName` varchar(45) NOT NULL,
  `UserPass` varchar(45) NOT NULL,
  `UserPage` varchar(45) NOT NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE KEY `UserPage_UNIQUE` (`UserPage`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

CREATE TABLE `trialdb.userpage` (
  `idUserPage` int(11) NOT NULL AUTO_INCREMENT,
  `PageName` varchar(45) DEFAULT NULL,
  `Theme` varchar(500) DEFAULT 'White',
  `imgBool` varchar(1) DEFAULT '0',
  PRIMARY KEY (`idUserPage`),
  KEY `PageName_idx` (`PageName`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

