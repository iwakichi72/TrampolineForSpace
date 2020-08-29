<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\UserScore $userScore
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Html->link(__('Edit User Score'), ['action' => 'edit', $userScore->id]) ?> </li>
        <li><?= $this->Form->postLink(__('Delete User Score'), ['action' => 'delete', $userScore->id], ['confirm' => __('Are you sure you want to delete # {0}?', $userScore->id)]) ?> </li>
        <li><?= $this->Html->link(__('List User Score'), ['action' => 'index']) ?> </li>
        <li><?= $this->Html->link(__('New User Score'), ['action' => 'add']) ?> </li>
    </ul>
</nav>
<div class="userScore view large-9 medium-8 columns content">
    <h3><?= h($userScore->id) ?></h3>
    <table class="vertical-table">
        <tr>
            <th scope="row"><?= __('UserName') ?></th>
            <td><?= h($userScore->userName) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('Id') ?></th>
            <td><?= $this->Number->format($userScore->id) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('HeightRecord') ?></th>
            <td><?= $this->Number->format($userScore->heightRecord) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('GameScore') ?></th>
            <td><?= $this->Number->format($userScore->gameScore) ?></td>
        </tr>
        <tr>
            <th scope="row"><?= __('RegDate') ?></th>
            <td><?= h($userScore->regDate) ?></td>
        </tr>
    </table>
</div>
