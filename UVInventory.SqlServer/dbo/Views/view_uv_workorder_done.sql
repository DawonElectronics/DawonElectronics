CREATE VIEW dbo.view_uv_workorder_done AS
SELECT tut.end_customer,tuw.lotid,tut.cust_modelname,tut.cust_revision,tut.cust_toolno,tut.mes_prc_name,
tp.prc_name,tut.worksize_x,tut.worksize_y,tuw.create_time,tut.[depth],tuw.pnlqty,
tuw.txid,tu_in.user_name as trackin_username,tuw.trackin_time,tu_out.user_name as trackout_username,tuw.trackout_time,tuw.lot_type,
 
tuw.lot_notes,tut.hole_count,tuw.machine_cs,tuw.machine_ss,tut.sample,
tut.product_id,tut.array_blk,tut.pcs,tut.layer,tut.create_date, CONCAT(tut.PrcLayerFrom_1 ,'-', tut.PrcLayerTo_1) as prc_layer1,CONCAT(tut.PrcLayerFrom_2 ,'-', tut.PrcLayerTo_2) as prc_layer2,
tut.mes_prc_code,tut.product_type,tut.tool_notes,tut.cust_comment,
tut.insul_type,tut.main_hole_size,tut.mes_seq_code,
tp.prc_code,
tuw.isDone,tuw.isPrinted,tuw.id,tc.cust_name,tuw.sample_order
from (select * FROM tb_uv_workorder wo WHERE wo.isDone = 1) tuw 
INNER JOIN tb_uv_toolinfo tut 
	ON tuw.product_id = tut.product_id
LEFT JOIN tb_prctype tp 
	ON tut.prc_code = tp.prc_code 
LEFT JOIN tb_customer tc 
	ON tut.cust_id = tc.cust_id 
LEFT JOIN tb_users tu_in
	ON tuw.trackin_user_id = tu_in.user_id 
LEFT JOIN tb_users tu_out
	ON tuw.trackout_user_id = tu_out.user_id 	
