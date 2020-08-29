<?php
namespace App\Controller;

use App\Controller\AppController;

/**
 * UserScore Controller
 *
 * @property \App\Model\Table\UserScoreTable $UserScore
 *
 * @method \App\Model\Entity\UserScore[]|\Cake\Datasource\ResultSetInterface paginate($object = null, array $settings = [])
 */
class UserScoreController extends AppController
{

    /*
    リクエストURL
    http://localhost/tfs/UserScore/getRanking
    リクエストパラメータ
    無し
    レスポンスコード
    ランキングデータリスト(json)
    */
	//ランキングデータリストを取得する。
	public function getRanking()
	{
		$this->autoRender	= false;
		
		//テーブルからランキングリストをとってくる
        $query	= $this->UserScore->find("all");

        //クエリー処理を行う。
        $query->order(['gameScore'=>'DESC']); //昇順
        $query->limit(10);                   //取得件数を10件までに絞る
		
		//jsonにシリアライズする。
		$json	= json_encode($query);

		//jsonデータを返す。（レスポンスとして表示する。）
		echo $json;
    }

    /*
    リクエストURL
    http://localhost/tfs/UserScore/setRanking
    リクエストパラメータ
    name  varchar
    score int
    レスポンスコード
    failed:失敗
    success:成功
    */
    //ランキングデータ単体をセットする。
	public function setRanking()
	{
		$this->autoRender = false;

        //POST パラメータを取得
        $postUserName   = $this->request->data["userName"];
        $postHeightRecord  = $this->request->data["heightRecord"];
        $postGameScore  = $this->request->data["gameScore"];

        //テーブルに追加するレコードを作る
        $record = array(
            "userName"=>$postUserName,
            "heightRecord"=>$postHeightRecord,
            "gameScore"=>$postGameScore,
            "regDate"=>date("Y/m/d H:i:s")
        );

        //テーブルにレコードを追加
        $prm1    = $this->UserScore->newEntity();
        $prm2    = $this->UserScore->patchEntity($prm1,$record, ['validate' => false]);
        if( $this->UserScore->save($prm2) ){
            echo "success";   //成功
        }else{
            echo "failed";   //失敗
        }
	}
}
