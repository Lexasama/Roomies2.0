package fr.intech.roomies.model.finance;

import fr.intech.roomies.model.people.Roomie;

import javax.persistence.*;
import java.util.Date;


@Entity
public class Transaction {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int transactionId;

    private String Descriptionn;

    @Column(nullable = false)
    private Date date;

    @Column(nullable = false)
    private int amount;

    public Transaction(){}

    public int getTransactionId() {
        return transactionId;
    }

    public void setTransactionId(int transactionId) {
        this.transactionId = transactionId;
    }

    public String getDescriptionn() {
        return Descriptionn;
    }

    public void setDescriptionn(String descriptionn) {
        Descriptionn = descriptionn;
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
}
