package fr.intech.roomies.model.finance;

import javax.persistence.*;
import java.util.Date;

@Entity
public class Spending {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int spendingId;

    @Column(nullable = false)
    private Date date;

    @Column(nullable = false)
    private int amount;

    private String description;

    public Spending(){}

    public int getSpendingId() {
        return spendingId;
    }

    public void setSpendingId(int spendingId) {
        this.spendingId = spendingId;
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
