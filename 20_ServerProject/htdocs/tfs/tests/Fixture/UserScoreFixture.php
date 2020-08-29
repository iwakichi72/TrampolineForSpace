<?php
namespace App\Test\Fixture;

use Cake\TestSuite\Fixture\TestFixture;

/**
 * UserScoreFixture
 *
 */
class UserScoreFixture extends TestFixture
{

    /**
     * Table name
     *
     * @var string
     */
    public $table = 'user_score';

    /**
     * Fields
     *
     * @var array
     */
    // @codingStandardsIgnoreStart
    public $fields = [
        'id' => ['type' => 'integer', 'length' => 10, 'unsigned' => false, 'null' => false, 'default' => null, 'comment' => 'データID', 'autoIncrement' => true, 'precision' => null],
        'userName' => ['type' => 'string', 'length' => 30, 'null' => false, 'default' => null, 'collate' => 'utf8_general_ci', 'comment' => 'ユーザー名', 'precision' => null, 'fixed' => null],
        'heightRecord' => ['type' => 'integer', 'length' => 10, 'unsigned' => false, 'null' => false, 'default' => null, 'comment' => 'ジャンプ最高記録', 'precision' => null, 'autoIncrement' => null],
        'gameScore' => ['type' => 'integer', 'length' => 10, 'unsigned' => false, 'null' => false, 'default' => null, 'comment' => 'ゲームスコア', 'precision' => null, 'autoIncrement' => null],
        'regDate' => ['type' => 'timestamp', 'length' => null, 'null' => false, 'default' => 'current_timestamp()', 'comment' => '登録日', 'precision' => null],
        '_constraints' => [
            'primary' => ['type' => 'primary', 'columns' => ['id'], 'length' => []],
        ],
        '_options' => [
            'engine' => 'InnoDB',
            'collation' => 'utf8mb4_general_ci'
        ],
    ];
    // @codingStandardsIgnoreEnd

    /**
     * Init method
     *
     * @return void
     */
    public function init()
    {
        $this->records = [
            [
                'id' => 1,
                'userName' => 'Lorem ipsum dolor sit amet',
                'heightRecord' => 1,
                'gameScore' => 1,
                'regDate' => 1597835880
            ],
        ];
        parent::init();
    }
}
