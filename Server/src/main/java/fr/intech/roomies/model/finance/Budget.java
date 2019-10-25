package fr.intech.roomies.model.finance;

import javax.persistence.*;
import java.util.Date;

@Entity
public class Budget {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int budgetId;

    @Column(nullable = false)
    private int amount;

    @Column(nullable = false)
    private Date d1;

    @Column(nullable = false)
    private Date d2;

    @Column(nullable = false)
    private int  categoryId;

    public Budget(){}

    public void setBudgetId(int budgetId) {
        this.budgetId = budgetId;
    }

    public int getAmount() {
        return amount;
    }

    public void setAmount(int amount) {
        this.amount = amount;
    }

    public Date getD1() {
        return d1;
    }

    public void setD1(Date d1) {
        this.d1 = d1;
    }

    public Date getD2() {
        return d2;
    }

    public void setD2(Date d2) {
        this.d2 = d2;
    }

    public int getBudgetId() {
        return budgetId;
    }
}
