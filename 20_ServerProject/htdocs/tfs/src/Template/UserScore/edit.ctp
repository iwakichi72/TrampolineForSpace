<?php
/**
 * @var \App\View\AppView $this
 * @var \App\Model\Entity\UserScore $userScore
 */
?>
<nav class="large-3 medium-4 columns" id="actions-sidebar">
    <ul class="side-nav">
        <li class="heading"><?= __('Actions') ?></li>
        <li><?= $this->Form->postLink(
                __('Delete'),
                ['action' => 'delete', $userScore->id],
                ['confirm' => __('Are you sure you want to delete # {0}?', $userScore->id)]
            )
        ?></li>
        <li><?= $this->Html->link(__('List User Score'), ['action' => 'index']) ?></li>
    </ul>
</nav>
<div class="userScore form large-9 medium-8 columns content">
    <?= $this->Form->create($userScore) ?>
    <fieldset>
        <legend><?= __('Edit User Score') ?></legend>
        <?php
            echo $this->Form->control('userName');
            echo $this->Form->control('heightRecord');
            echo $this->Form->control('gameScore');
            echo $this->Form->control('regDate');
        ?>
    </fieldset>
    <?= $this->Form->button(__('Submit')) ?>
    <?= $this->Form->end() ?>
</div>
