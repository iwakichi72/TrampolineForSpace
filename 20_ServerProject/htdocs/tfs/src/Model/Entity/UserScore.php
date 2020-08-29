<?php
namespace App\Model\Entity;

use Cake\ORM\Entity;

/**
 * UserScore Entity
 *
 * @property int $id
 * @property string $userName
 * @property int $heightRecord
 * @property int $gameScore
 * @property \Cake\I18n\FrozenTime $regDate
 */
class UserScore extends Entity
{

    /**
     * Fields that can be mass assigned using newEntity() or patchEntity().
     *
     * Note that when '*' is set to true, this allows all unspecified fields to
     * be mass assigned. For security purposes, it is advised to set '*' to false
     * (or remove it), and explicitly make individual fields accessible as needed.
     *
     * @var array
     */
    protected $_accessible = [
        'userName' => true,
        'heightRecord' => true,
        'gameScore' => true,
        'regDate' => true
    ];
}
