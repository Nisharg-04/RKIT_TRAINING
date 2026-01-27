CREATE DATABASE  IF NOT EXISTS `invdb01` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `invdb01`;
-- MySQL dump 10.13  Distrib 8.0.27, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: invdb01
-- ------------------------------------------------------
-- Server version	8.0.27

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `prdtb01`
--

DROP TABLE IF EXISTS `prdtb01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prdtb01` (
  `prdf01` int NOT NULL AUTO_INCREMENT,
  `prdf02` varchar(150) NOT NULL,
  `prdf03` decimal(10,2) NOT NULL,
  `prdf04` int NOT NULL,
  `prdf05` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`prdf01`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prdtb01`
--

LOCK TABLES `prdtb01` WRITE;
/*!40000 ALTER TABLE `prdtb01` DISABLE KEYS */;
INSERT INTO `prdtb01` VALUES (1,'string',100.00,20,'2026-01-15 17:16:28');
/*!40000 ALTER TABLE `prdtb01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usrtb01`
--

DROP TABLE IF EXISTS `usrtb01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usrtb01` (
  `usrf01` int NOT NULL AUTO_INCREMENT,
  `usrf02` varchar(100) NOT NULL,
  `usrf03` varchar(255) NOT NULL,
  `usrf04` varchar(50) NOT NULL,
  `usrf05` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`usrf01`),
  UNIQUE KEY `usrf02` (`usrf02`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usrtb01`
--

LOCK TABLES `usrtb01` WRITE;
/*!40000 ALTER TABLE `usrtb01` DISABLE KEYS */;
INSERT INTO `usrtb01` VALUES (1,'admin','admin123','Admin','2026-01-15 16:17:15'),(2,'manager','manager123','Manager','2026-01-15 16:17:15'),(3,'staff','staff123','Staff','2026-01-15 16:17:15');
/*!40000 ALTER TABLE `usrtb01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'invdb01'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-01-26 14:51:46
