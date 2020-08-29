<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\UserScore[]|\Cake\Collection\CollectionInterface $userScore
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('New User Score'), ['action' => 'add']) ?></li>
    </ul>
</nav>
<div class="userScore index large-9 medium-8 columns content">
    <h3><?= __('User Score') ?></h3>
    <table cellpadding="0" cellspacing="0">
        <thead>
            <tr>
                <th scope="col"><?= $this->Paginator->sort('id') ?></th>
                <th scope="col"><?= $this->Paginator->sort('userName') ?></th>
                <th scope="col"><?= $this->Paginator->sort('heightRecord') ?></th>
                <th scope="col"><?= $this->Paginator->sort('gameScore') ?></th>
                <th scope="col"><?= $this->Paginator->sort('regDate') ?></th>
                <th scope="col" class="actions"><?= __('Actions') ?></th>
            </tr>
        </thead>
        <tbody>
            <?php foreach ($userScore as $userScore): ?>
            <tr>
                <td><?= $this->Number->format($userScore->id) ?></td>
                <td><?= h($userScore->userName) ?></td>
                <td><?= $this->Number->format($userScore->heightRecord) ?></td>
                <td><?= $this->Number->format($userScore->gameScore) ?></td>
                <td><?= h($userScore->regDate) ?></td>
                <td class="actions">
                    <?= $this->Html->link(__('View'), ['action' => 'view', $userScore->id]) ?>
                    <?= $this->Html->link(__('Edit'), ['action' => 'edit', $userScore->id]) ?>
                    <?= $this->Form->postLink(__('Delete'), ['action' => 'delete', $userScore->id], ['confirm' => __('Are you sure you want to delete # {0}?', $userScore->id)]) ?>
                </td>
            </tr>
            <?php endforeach; ?>
        </tbody>
    </table>
    <div class="paginator">
        <ul class="pagination">
            <?= $this->Paginator->first('<< ' . __('first')) ?>
            <?= $this->Paginator->prev('< ' . __('previous')) ?>
            <?= $this->Paginator->numbers() ?>
            <?= $this->Paginator->next(__('next') . ' >') ?>
            <?= $this->Paginator->last(__('last') . ' >>') ?>
        </ul>
        <p><?= $this->Paginator->counter(['format' => __('Page {{page}} of {{pages}}, showing {{current}} record(s) out of {{count}} total')]) ?></p>
    </div>
</div>
