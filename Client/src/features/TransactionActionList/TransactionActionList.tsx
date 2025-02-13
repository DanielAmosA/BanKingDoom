import React from 'react'
import { ITransactionActionListProps } from '../../Interfaces/Props/ITransactionActionListProps'
import './TransactionActionList.scss';
import { ITransactionActionWithAPIResult } from '../../Interfaces/Entity/ITransactionActionWithAPIResult';
export const TransactionActionList: React.FC<{ transactionActionListProps : ITransactionActionListProps}> = (
    {
      transactionActionListProps
    }
) => {
    return (
        <div className="tranList">
          <h3 className='tranListTopTitle'>היסטורית הפעולות שבוצעו : </h3>
          <ul className='tranListMain'>
            {transactionActionListProps.transactionsAction.map((transactionAction:ITransactionActionWithAPIResult) => (
              <li key={transactionAction.id} className={`tranItem ${transactionAction.transactionType === "Deposit" ? "tranItemInDeposit" : "tranItemInWithdrawal"}`}>
                <div className="tranItemData">
                  <span className='tranItemDataType'>
                    {transactionAction.transactionType === "Deposit" ? "🪙" : "💸"} <strong>
                      {transactionAction.transactionType  === "Deposit" ? 'הפקדה' : 'משיכה'}</strong>
                  </span>
                  <span className="tranItemDataAmount">
                  <strong>סכום:</strong> {transactionAction.amount} ₪
                  </span>
                  <span className="tranItemDataDate"><strong>תאריך:</strong> {new Date(transactionAction.createdAt!).toLocaleDateString("he-IL")}</span>
                  <span className="tranItemDataStatus"><strong>סטטוס:</strong> {transactionAction.statusAction}</span>
                  <span className="tranItemDataStatus"><strong>חשבון בנק:</strong> {transactionAction.bankAccountNumber}</span>
                </div>
                <div className="tranItemBtns">
                  <button className="tranItemBtn tranItemEditBtn" onClick={() => transactionActionListProps.onEdit(transactionAction)}>✍️ ערוך</button>
                  <button className="tranItemBtn tranItemDeleteBtn" onClick={() => transactionActionListProps.onDelete(transactionAction.id!)}>🗑️ מחק</button>
                </div>
              </li>
            ))}
          </ul>
          {transactionActionListProps.transactionsAction.length === 0 && (
            <div className="tranListNotHaveData">
             💸 אין פעולות כרגע...
            </div>
          )}
        </div>
      );
}