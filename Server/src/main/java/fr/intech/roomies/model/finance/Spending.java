package fr.intech.roomies.model.finance;

import javax.persistence.Entity;
import javax.persistence.Id;
import java.util.Date;

@Entity
public class Spending {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int spendingId;

    @ElementCollection
    private List<Budget> budgets;

    private Date date;
    private int amount;
    private String description;

    public Spending(){}

    public int getSpendingId() {
        return spendingId;
    }

    public void setSpendingId(int spendingId) {
        this.spendingId = spendingId;
    }

    public List<Budget> getBudgets() {
        return budgets;
    }

    public void setBudgets(List<Budget> budgets) {
        this.budgets = budgets;
    }

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public int getAmount() {
        return amount;
    }

    public void setAmount(int amount) {
        this.amount = amount;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }
}
