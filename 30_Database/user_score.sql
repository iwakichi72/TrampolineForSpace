-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- ホスト: 127.0.0.1
-- 生成日時: 2020-08-19 16:18:48
-- サーバのバージョン： 10.4.13-MariaDB
-- PHP のバージョン: 7.2.32

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- データベース: `tfs`
--

-- --------------------------------------------------------

--
-- テーブルの構造 `user_score`
--

CREATE TABLE `user_score` (
  `id` int(10) NOT NULL COMMENT 'データID',
  `userName` varchar(30) CHARACTER SET utf8 NOT NULL COMMENT 'ユーザー名',
  `heightRecord` int(10) NOT NULL COMMENT 'ジャンプ最高記録',
  `gameScore` int(10) NOT NULL COMMENT 'ゲームスコア',
  `regDate` timestamp NOT NULL DEFAULT current_timestamp() COMMENT '登録日'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- テーブルのデータのダンプ `user_score`
--

INSERT INTO `user_score` (`id`, `userName`, `heightRecord`, `gameScore`, `regDate`) VALUES
(1, 'test', 26, 26, '2020-08-19 05:02:48'),

--
-- ダンプしたテーブルのインデックス
--

--
-- テーブルのインデックス `user_score`
--
ALTER TABLE `user_score`
  ADD PRIMARY KEY (`id`);

--
-- ダンプしたテーブルのAUTO_INCREMENT
--

--
-- テーブルのAUTO_INCREMENT `user_score`
--
ALTER TABLE `user_score`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT COMMENT 'データID', AUTO_INCREMENT=18;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
