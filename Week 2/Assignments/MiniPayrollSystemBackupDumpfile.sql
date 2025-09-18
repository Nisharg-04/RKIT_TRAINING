-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: localhost    Database: minipayrolldb
-- ------------------------------------------------------
-- Server version	8.0.43

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
-- Table structure for table `prla01`
--

DROP TABLE IF EXISTS `prla01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prla01` (
  `t04f01` int NOT NULL AUTO_INCREMENT,
  `t04f02` int DEFAULT NULL,
  `t04f03` decimal(10,2) DEFAULT NULL,
  `t04f04` decimal(10,2) DEFAULT NULL,
  `t04f05` datetime DEFAULT CURRENT_TIMESTAMP,
  `t04f06` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`t04f01`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prla01`
--

LOCK TABLES `prla01` WRITE;
/*!40000 ALTER TABLE `prla01` DISABLE KEYS */;
INSERT INTO `prla01` VALUES (1,1,90000.00,92000.00,'2025-09-18 13:09:55','root@localhost');
/*!40000 ALTER TABLE `prla01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prlm01`
--

DROP TABLE IF EXISTS `prlm01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prlm01` (
  `t01f01` int NOT NULL AUTO_INCREMENT,
  `t01f02` varchar(100) NOT NULL,
  PRIMARY KEY (`t01f01`),
  UNIQUE KEY `t01f02` (`t01f02`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prlm01`
--

LOCK TABLES `prlm01` WRITE;
/*!40000 ALTER TABLE `prlm01` DISABLE KEYS */;
INSERT INTO `prlm01` VALUES (2,'Human Resources'),(3,'Sales'),(1,'Technology');
/*!40000 ALTER TABLE `prlm01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prlm02`
--

DROP TABLE IF EXISTS `prlm02`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prlm02` (
  `t02f01` int NOT NULL AUTO_INCREMENT,
  `t02f02` varchar(150) NOT NULL,
  `t02f03` int DEFAULT NULL,
  `t02f04` decimal(10,2) NOT NULL,
  PRIMARY KEY (`t02f01`),
  KEY `t02f03` (`t02f03`),
  KEY `idx_emp_name` (`t02f02`),
  CONSTRAINT `prlm02_ibfk_1` FOREIGN KEY (`t02f03`) REFERENCES `prlm01` (`t01f01`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prlm02`
--

LOCK TABLES `prlm02` WRITE;
/*!40000 ALTER TABLE `prlm02` DISABLE KEYS */;
INSERT INTO `prlm02` VALUES (1,'Aarav Sharma',1,92000.00),(2,'Priya Singh',1,95000.00),(3,'Rohan Mehta',3,75000.00),(4,'Aditi Gupta',3,80000.00),(5,'Vikram Rathore',2,65000.00);
/*!40000 ALTER TABLE `prlm02` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `trg_after_employee_salary_update` AFTER UPDATE ON `prlm02` FOR EACH ROW BEGIN
    -- Check if the salary value has actually changed
    IF OLD.t02f04 <> NEW.t02f04 THEN
        INSERT INTO prla01(t04f02, t04f03, t04f04, t04f06)
        VALUES (OLD.t02f01, OLD.t02f04, NEW.t02f04, USER());
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `prlt01`
--

DROP TABLE IF EXISTS `prlt01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prlt01` (
  `t03f01` int NOT NULL AUTO_INCREMENT,
  `t03f02` int DEFAULT NULL,
  `t03f03` varchar(20) NOT NULL,
  `t03f04` decimal(10,2) NOT NULL,
  `t03f05` date DEFAULT NULL,
  PRIMARY KEY (`t03f01`),
  KEY `t03f02` (`t03f02`),
  CONSTRAINT `prlt01_ibfk_1` FOREIGN KEY (`t03f02`) REFERENCES `prlm02` (`t02f01`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prlt01`
--

LOCK TABLES `prlt01` WRITE;
/*!40000 ALTER TABLE `prlt01` DISABLE KEYS */;
INSERT INTO `prlt01` VALUES (1,1,'January',90000.00,'2025-01-31'),(2,2,'January',95000.00,'2025-01-31'),(3,3,'January',75000.00,'2025-01-31'),(4,1,'February',90000.00,'2025-02-28'),(5,2,'February',95000.00,'2025-02-28'),(6,1,'November',92000.00,'2025-10-05'),(7,2,'November',95000.00,'2025-10-05'),(8,3,'November',75000.00,'2025-10-05'),(9,4,'November',80000.00,'2025-10-05'),(10,5,'November',65000.00,'2025-10-05');
/*!40000 ALTER TABLE `prlt01` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `vw_employee_summary`
--

DROP TABLE IF EXISTS `vw_employee_summary`;
/*!50001 DROP VIEW IF EXISTS `vw_employee_summary`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vw_employee_summary` AS SELECT 
 1 AS `employee_id`,
 1 AS `employee_name`,
 1 AS `employee_salary`,
 1 AS `department_name`*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping events for database 'minipayrolldb'
--

--
-- Dumping routines for database 'minipayrolldb'
--
/*!50003 DROP FUNCTION IF EXISTS `ufn_CalculatePerformanceBonus` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `ufn_CalculatePerformanceBonus`(p_salary DECIMAL(10,2)) RETURNS decimal(10,2)
    DETERMINISTIC
BEGIN
    
    DECLARE v_bonus DECIMAL(10,2);
    SET v_bonus = p_salary * 0.10; -- 10% of monthly salary
    RETURN v_bonus;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `ufn_CalculateTax` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `ufn_CalculateTax`(salary DECIMAL(10,2)) RETURNS decimal(10,2)
    DETERMINISTIC
BEGIN
    DECLARE tax DECIMAL(10,2);
    IF salary > 70000 THEN SET tax = salary * 0.20;
    ELSEIF salary > 50000 THEN SET tax = salary * 0.10;
    ELSE SET tax = salary * 0.05;
    END IF;
    RETURN tax;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `usp_CalculateYearlySalary` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_CalculateYearlySalary`(IN p_emp_id INT, OUT p_yearly_salary DECIMAL(12, 2))
BEGIN
    DECLARE v_monthly_salary DECIMAL(10, 2);

    SELECT t02f04 INTO v_monthly_salary FROM prlm02 WHERE t02f01 = p_emp_id;

    IF v_monthly_salary IS NULL THEN
        SET p_yearly_salary = NULL; -- Indicates employee not found
    ELSE
        SET p_yearly_salary = v_monthly_salary * 12;
    END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `usp_RunMonthlyPayroll` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_RunMonthlyPayroll`(IN p_month VARCHAR(20), IN p_pay_date DATE)
BEGIN
    DECLARE done INT DEFAULT FALSE;
    DECLARE v_emp_id INT;
    DECLARE v_salary DECIMAL(10, 2);
    
    DECLARE emp_cursor CURSOR FOR SELECT t02f01, t02f04 FROM prlm02;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'An error occurred. Payroll run has been rolled back.' AS result;
    END;

    START TRANSACTION;
    
    OPEN emp_cursor;
    read_loop: LOOP
        FETCH emp_cursor INTO v_emp_id, v_salary;
        IF done THEN
            LEAVE read_loop;
        END IF;
        
        INSERT INTO prlt01 (t03f02, t03f03, t03f04, t03f05)
        VALUES (v_emp_id, p_month, v_salary, p_pay_date);
    END LOOP;
    
    CLOSE emp_cursor;
    COMMIT;
    SELECT 'Payroll run completed successfully.' AS result;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `vw_employee_summary`
--

/*!50001 DROP VIEW IF EXISTS `vw_employee_summary`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vw_employee_summary` AS select `emp`.`t02f01` AS `employee_id`,`emp`.`t02f02` AS `employee_name`,`emp`.`t02f04` AS `employee_salary`,`dept`.`t01f02` AS `department_name` from (`prlm02` `emp` join `prlm01` `dept` on((`emp`.`t02f03` = `dept`.`t01f01`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-09-18 15:43:25
