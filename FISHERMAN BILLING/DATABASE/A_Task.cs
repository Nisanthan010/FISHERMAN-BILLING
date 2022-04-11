using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FISHERMAN_BILLING.DATABASE
{
   public class A_Task
    {
        public SQLiteAsyncConnection table_dp;
        public A_Task(string DatabasePath)
        {
            table_dp = new SQLiteAsyncConnection(DatabasePath);

          /* table_dp.CloseAsync();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete(DatabasePath);*/

            Table_make();
        }
        private void Table_make()
        {
            _ = table_dp.CreateTableAsync<B_kg_entry_date>();
            _ = table_dp.CreateTableAsync<B_DateOnly>();
            _ = table_dp.CreateTableAsync<Reference_Customer>();
            _ = table_dp.CreateTableAsync<B_customerdetail_save>();
            _ = table_dp.CreateTableAsync<E_PetrolReduction>();
            _ = table_dp.CreateTableAsync<C_TotalWithoutReduction>();
            _ = table_dp.CreateTableAsync<E_worker_reduction>();
            _ = table_dp.CreateTableAsync<E_extra_reduction>();
            _ = table_dp.CreateTableAsync<Final_Detail>();
            _ = table_dp.CreateTableAsync<Reference_statment>();
        }
        #region GET TABLE
        public async Task<List<B_DateOnly>> Get_DateOnly()
        {
            return await table_dp.QueryAsync<B_DateOnly>("SELECT * FROM B_DateOnly");
        }
        public async Task<List<Reference_Customer>> Get_Reference_Customer()
        {
            return await table_dp.QueryAsync<Reference_Customer>("SELECT * FROM Reference_Customer");
        }
        public async Task<List<B_kg_entry_date>> Get_entry_kg_pass_only()
        {
            return await table_dp.QueryAsync<B_kg_entry_date>("SELECT * FROM B_kg_entry_date");
        }
        public async Task<List<B_customerdetail_save>> Get_customerdetail_save()
        {
            return await table_dp.QueryAsync<B_customerdetail_save>("SELECT * FROM B_customerdetail_save");
        }
        public async Task<List<E_PetrolReduction>> Get_petrol()
        {
            return await table_dp.QueryAsync<E_PetrolReduction>("SELECT * FROM E_PetrolReduction");
        }
        public async Task<List<C_TotalWithoutReduction>> Get_totalWithOutReduction()
        {
            return await table_dp.QueryAsync<C_TotalWithoutReduction>("SELECT * FROM C_TotalWithoutReduction");
        }
        public async Task<List<E_worker_reduction>> Get_worker_reduction()
        {
            return await table_dp.QueryAsync<E_worker_reduction>("SELECT * FROM E_worker_reduction");
        }
        public async Task<List<E_extra_reduction>> Get_extra_reduction()
        {
            return await table_dp.QueryAsync<E_extra_reduction>("SELECT * FROM E_extra_reduction");
        }
        public async Task<List<Final_Detail>> Get_finalDetail()
        {
            return await table_dp.QueryAsync<Final_Detail>("SELECT * FROM Final_Detail");
        }
        public async Task<List<Reference_statment>> Get_reference_statment_Detail()
        {
            return await table_dp.QueryAsync<Reference_statment>("SELECT * FROM Reference_statment");
        }
        #endregion
        #region ADD TABLE
        public async Task<int> Add_date(B_DateOnly now_date)
        {
            return await table_dp.InsertAsync(now_date);
        }
        public async Task<int> Add_Reference_Customer(Reference_Customer Kg)
        {
            return await table_dp.InsertAsync(Kg);
        }
        public async Task<int> Add_Entry_kg_pass_one(B_kg_entry_date Kg)
        {
            return await table_dp.InsertAsync(Kg);
        }
        public async Task<int> Add_B_customerdetail_save(B_customerdetail_save Customer)
        {
            return await table_dp.InsertAsync(Customer);
        }
        public async Task<int> Add_petrol(E_PetrolReduction ConvertintoAmount)
        {
            return await table_dp.InsertAsync(ConvertintoAmount);
        }
        public async Task<int> Add_C_TotalWithoutReduction(C_TotalWithoutReduction TotalWithoutReduction)
        {
            return await table_dp.InsertAsync(TotalWithoutReduction);
        }
        public async Task<int> Add_E_worker_reduction(E_worker_reduction Worker_reduction)
        {
            return await table_dp.InsertAsync(Worker_reduction);
        }
        public async Task<int> Add_E_extra_reduction(E_extra_reduction Extra_reduction)
        {
            return await table_dp.InsertAsync(Extra_reduction);
        }
        public async Task<int> Add_Final_Detail(Final_Detail Finaldetail)
        {
            return await table_dp.InsertAsync(Finaldetail);
        }
        public async Task<int> Add_reference_statment_Detail(Reference_statment Statment)           
        {
            return await table_dp.InsertAsync(Statment);
        }
        #endregion
        #region EDIT
        public async Task<int> Update_now_date(B_DateOnly now_date)
        {
            return await table_dp.UpdateAsync(now_date);
        }
        public async Task<int> Update_Reference_Customer(Reference_Customer Kg)
        {
            return await table_dp.UpdateAsync(Kg);
        }
        public async Task<int> Update_B_customerdetail_save(B_customerdetail_save Customer)
        {
            return await table_dp.UpdateAsync(Customer);
        }
        public async Task<int> Update_C_petrol(E_PetrolReduction ConvertintoAmount)
        {
            return await table_dp.UpdateAsync(ConvertintoAmount);
        }
        public async Task<int> Update_C_TotalWithoutReduction(C_TotalWithoutReduction Reduction)
        {
            return await table_dp.UpdateAsync(Reduction);
        }
        public async Task<int> Update_E_worker_reduction(E_worker_reduction Reduction)
        {
            return await table_dp.UpdateAsync(Reduction);
        }
        public async Task<int> Update_E_extra_reduction(E_extra_reduction Reduction)
        {
            return await table_dp.UpdateAsync(Reduction);
        }
        public async Task<int> Update_Final_Detail(Final_Detail Finaldetail)
        {
            return await table_dp.UpdateAsync(Finaldetail);
        }
        #endregion
        #region DELETE
        public async Task<int> Delete_now_date(B_DateOnly now_date)
        {
            return await table_dp.DeleteAsync(now_date);
        }
        public async Task<int> Delete_kg_pass(B_kg_entry_date kg)
        {
            return await table_dp.DeleteAsync(kg);
        }
        public async Task<int> Delete_Reference_Customer(Reference_Customer Kg)
        {
            return await table_dp.DeleteAsync(Kg);
        }
        public async Task<int> DeleteKg(B_DateOnly now_date)
        {
            return await table_dp.DeleteAsync(now_date);
        }
        public async Task<int> Delete_B_customerdetail_save(B_customerdetail_save Customer)
        {
            return await table_dp.DeleteAsync(Customer);
        }
        public async Task<int> Delete_petrol(E_PetrolReduction ConvertintoAmount)
        {
            return await table_dp.DeleteAsync(ConvertintoAmount);
        }
        public async Task<int> Delete_C_TotalWithoutReduction(C_TotalWithoutReduction Reduction)
        {
            return await table_dp.DeleteAsync(Reduction);
        }
        public async Task<int> Delete_E_worker_reduction(E_worker_reduction Reduction)
        {
            return await table_dp.DeleteAsync(Reduction);
        }
        public async Task<int> DeleteKg_Final_Detail(Final_Detail Finaldetail)
        {
            return await table_dp.DeleteAsync(Finaldetail);
        }
        #endregion
        #region DELETE ALL
        public async Task<List<B_DateOnly>> Delete_all_DateOnly()
        {
            return await table_dp.QueryAsync<B_DateOnly>("DELETE FROM B_DateOnly");
        }
        public async Task<List<Reference_Customer>> Delete_all_Reference_Customer()
        {
            return await table_dp.QueryAsync<Reference_Customer>("DELETE  FROM Reference_Customer");
        }
        public async Task<List<B_kg_entry_date>> Delete_all_entry_kg_pass_one()
        {
            return await table_dp.QueryAsync<B_kg_entry_date>("DELETE  FROM B_kg_entry_date");
        }
        public async Task<List<B_customerdetail_save>> Delete_all_customerdetail_save()
        {
            return await table_dp.QueryAsync<B_customerdetail_save>("DELETE  FROM B_customerdetail_save");
        }
        public async Task<List<E_PetrolReduction>> Delete_all_petrol()
        {
            return await table_dp.QueryAsync<E_PetrolReduction>("DELETE  FROM E_PetrolReduction");
        }
        public async Task<List<C_TotalWithoutReduction>> Delete_all_totalWithOutReduction()
        {
            return await table_dp.QueryAsync<C_TotalWithoutReduction>("DELETE  FROM C_TotalWithoutReduction");
        }
        public async Task<List<E_worker_reduction>> Delete_all_worker_reduction()
        {
            return await table_dp.QueryAsync<E_worker_reduction>("DELETE  FROM E_worker_reduction");
        }
        public async Task<List<E_extra_reduction>> Delete_all_extra_reduction()
        {
            return await table_dp.QueryAsync<E_extra_reduction>("DELETE  FROM E_extra_reduction");
        }
        public async Task<List<Final_Detail>> Delete_all_finalDetail()
        {
            return await table_dp.QueryAsync<Final_Detail>("DELETE  FROM Final_Detail");
        }
        public async Task<List<Reference_statment>> Delete_all_reference_statment()
        {
            return await table_dp.QueryAsync<Reference_statment>("DELETE  FROM Reference_statment");
        }
        #endregion
    }
}

