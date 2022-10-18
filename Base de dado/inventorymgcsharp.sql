-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 18-Out-2022 às 00:39
-- Versão do servidor: 10.4.14-MariaDB
-- versão do PHP: 7.4.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `inventorymgcsharp`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `orders`
--

CREATE TABLE `orders` (
  `id` int(11) NOT NULL,
  `user` varchar(100) NOT NULL,
  `details` varchar(5000) NOT NULL,
  `price` varchar(50) NOT NULL,
  `paid` varchar(30) NOT NULL DEFAULT 'no'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `orders`
--

INSERT INTO `orders` (`id`, `user`, `details`, `price`, `paid`) VALUES
(4, 'roose', 'Model 2 Head Light manufactured 511*2', '511', 'cancelled'),
(5, 'roose', 'Model 1 Battery imported 224*1', '224', 'cancelled'),
(6, 'roose', 'Model 5 Steering Wheel imported 339*1', '339', 'cancelled'),
(7, 'demoaccount', 'DemoModel Demo Parts imported 255*3\r\n', '765', 'cancelled'),
(8, 'christine', 'Model 2 Seat Belt imported 88*1\r\n', '88', 'cancelled'),
(9, 'estela', 'Model 1 Spoiler imported 190*2\r\n', '380', 'cancelled'),
(10, 'robert', 'Model 1 Decklid imported 499*1\r\n', '499', 'cancelled'),
(11, 'jimmy', 'Model 3 Muffler manufactured 830*1\r\n', '830', 'cancelled'),
(12, 'cath', 'Model 4 Battery imported 201*1\r\n', '201', 'cancelled'),
(13, 'warren', 'Model 2 Seat Belt imported 88*2\r\n', '176', 'cancelled'),
(14, 'sacco', 'Model 2 Gear Lever imported 401*1\r\n', '401', 'cancelled'),
(15, 'demoaccount', 'Model 7 Indicator Lights imported 149*2\r\n', '298', 'cancelled'),
(16, 'demoaccount', 'Model 6 Dashcam imported 266*1\r\n', '266', 'cancelled'),
(17, 'arnaldo', 'Arroz chirrico imported 800*10\r\n', '8000', 'cancelled'),
(18, 'arnaldo', 'Arroz chirrico imported 800*20\r\n', '16000', 'cancelled'),
(19, 'arnaldo', 'Model 2 Head Light manufactured 511*4\r\n', '2044', 'cancelled'),
(20, 'arnaldo', 'Model 6 Dashcam imported 266*30\r\n', '7980', 'cancelled'),
(21, 'arnaldo', 'Massa Bela fabricado/A 1100*10\r\n', '11000', 'yes');

-- --------------------------------------------------------

--
-- Estrutura da tabela `spareparts`
--

CREATE TABLE `spareparts` (
  `id` int(11) NOT NULL,
  `model` varchar(100) NOT NULL,
  `part` varchar(100) NOT NULL,
  `type` varchar(50) NOT NULL,
  `price` varchar(50) NOT NULL,
  `instock` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `spareparts`
--

INSERT INTO `spareparts` (`id`, `model`, `part`, `type`, `price`, `instock`) VALUES
(25, 'Arroz', 'Chirrico', 'fabricado/A', '1500', 1000),
(26, 'Arroz', 'Sasseca', 'fabricado/A', '1200', 100),
(27, 'Oleo', 'Oliveira', 'importado/A', '500', 60),
(28, 'Oleo', 'Dona', 'fabricado/A', '450', 20),
(29, 'Massa', 'Bela', 'fabricado/A', '1100', 50);

-- --------------------------------------------------------

--
-- Estrutura da tabela `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `first` varchar(100) NOT NULL,
  `last` varchar(100) NOT NULL,
  `username` varchar(100) NOT NULL,
  `phone` varchar(10) NOT NULL,
  `password` varchar(50) NOT NULL,
  `usertype` varchar(20) NOT NULL DEFAULT 'member'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Extraindo dados da tabela `users`
--

INSERT INTO `users` (`id`, `first`, `last`, `username`, `phone`, `password`, `usertype`) VALUES
(1, 'Roosevelt', 'Mitchell', 'roose', '1450214580', 'ee11cbb19052e40b07aac0ca060c23ee', 'member'),
(2, 'Thom', 'Rodriquez', 'manager', '7854541250', '0795151defba7a4b5dfa89170de46277', 'manager'),
(3, 'Demo', 'Account', 'demoaccount', '7000000020', '5f4dcc3b5aa765d61d8327deb882cf99', 'member'),
(4, 'Christine', 'Moore', 'christine', '8521111101', '5f4dcc3b5aa765d61d8327deb882cf99', 'member'),
(5, 'Liam', 'Moore', 'admin', '1470001011', '482c811da5d5b4bc6d497ffa98491e38', 'admin'),
(6, 'Estela', 'Choate', 'estela', '1254785555', '5f4dcc3b5aa765d61d8327deb882cf99', 'member'),
(7, 'Robert', 'Grote', 'robert', '8520002014', '5f4dcc3b5aa765d61d8327deb882cf99', 'member'),
(8, 'Jimmy', 'Lucas', 'jimmy', '1478569800', '1a1dc91c907325c69271ddf0c944bc72', 'member'),
(9, 'Catherine', 'Lawrence', 'cath', '8522222220', '1a1dc91c907325c69271ddf0c944bc72', 'member'),
(10, 'Joseph', 'Warren', 'warren', '6545214500', '202cb962ac59075b964b07152d234b70', 'member'),
(11, 'Marian', 'Sacco', 'sacco', '1478545874', 'c6f057b86584942e415435ffb1fa93d4', 'member'),
(12, 'arnaldo', 'tomo', 'arnaldo', '846474', 'c6d6d5ed2701e77d49d8506ebe0d7fc2', 'member');

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `spareparts`
--
ALTER TABLE `spareparts`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `orders`
--
ALTER TABLE `orders`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de tabela `spareparts`
--
ALTER TABLE `spareparts`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT de tabela `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
