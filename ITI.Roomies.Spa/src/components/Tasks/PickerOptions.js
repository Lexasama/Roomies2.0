const pickerOptions = {
  disabledDate(time) {
    let date = new Date();
    date.setDate(date.getDate() - 1);
    return time.getTime() <= date;
  },
  shortcuts: [
    {
      text: "Today",
      onClick(picker) {
        picker.$emit("pick", new Date());
      }
    },
    {
      text: "Tomorrow",
      onClick(picker) {
        const date = new Date();
        date.setTime(date.getTime() + 3600 * 1000 * 24);
        picker.$emit("pick", date);
      }
    },
    {
      text: "In a week",
      onClick(picker) {
        const date = new Date();
        date.setTime(date.getTime() + 3600 * 1000 * 24 * 7);
        picker.$emit("pick", date);
      }
    }
  ]
};

export default pickerOptions;
