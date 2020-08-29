<?php
namespace App\Model\Table;

use Cake\ORM\Query;
use Cake\ORM\RulesChecker;
use Cake\ORM\Table;
use Cake\Validation\Validator;

/**
 * UserScore Model
 *
 * @method \App\Model\Entity\UserScore get($primaryKey, $options = [])
 * @method \App\Model\Entity\UserScore newEntity($data = null, array $options = [])
 * @method \App\Model\Entity\UserScore[] newEntities(array $data, array $options = [])
 * @method \App\Model\Entity\UserScore|bool save(\Cake\Datasource\EntityInterface $entity, $options = [])
 * @method \App\Model\Entity\UserScore|bool saveOrFail(\Cake\Datasource\EntityInterface $entity, $options = [])
 * @method \App\Model\Entity\UserScore patchEntity(\Cake\Datasource\EntityInterface $entity, array $data, array $options = [])
 * @method \App\Model\Entity\UserScore[] patchEntities($entities, array $data, array $options = [])
 * @method \App\Model\Entity\UserScore findOrCreate($search, callable $callback = null, $options = [])
 */
class UserScoreTable extends Table
{

    /**
     * Initialize method
     *
     * @param array $config The configuration for the Table.
     * @return void
     */
    public function initialize(array $config)
    {
        parent::initialize($config);

        $this->setTable('user_score');
        $this->setDisplayField('id');
        $this->setPrimaryKey('id');
    }

    /**
     * Default validation rules.
     *
     * @param \Cake\Validation\Validator $validator Validator instance.
     * @return \Cake\Validation\Validator
     */
    public function validationDefault(Validator $validator)
    {
        $validator
            ->integer('id')
            ->allowEmpty('id', 'create');

        $validator
            ->scalar('userName')
            ->maxLength('userName', 30)
            ->requirePresence('userName', 'create')
            ->notEmpty('userName');

        $validator
            ->integer('heightRecord')
            ->requirePresence('heightRecord', 'create')
            ->notEmpty('heightRecord');

        $validator
            ->integer('gameScore')
            ->requirePresence('gameScore', 'create')
            ->notEmpty('gameScore');

        $validator
            ->dateTime('regDate')
            ->requirePresence('regDate', 'create')
            ->notEmpty('regDate');

        return $validator;
    }
}
