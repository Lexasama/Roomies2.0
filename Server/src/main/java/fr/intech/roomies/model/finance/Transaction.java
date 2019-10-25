package fr.intech.roomies.model.finance;

import javax.persistence.*;
import java.util.Date;


@Entity
public class Transaction {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private int transactionId;

    private String Descriptionn;

    @Column(nullable = false)
    private int receiver;

    @Column(nullable = false)
    private int sender;

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

    public int getReceiver() {
        return receiver;
    }

    public void setReceiver(int receiver) {
        this.receiver = receiver;
    }

    public int getSender() {
        return sender;
    }

    public void setSender(int sender) {
        this.sender = sender;
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
