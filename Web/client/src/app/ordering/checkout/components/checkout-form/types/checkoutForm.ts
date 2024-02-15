export default interface IOrderRequest {
    firstname: string,
    secondname: string,
    patronimyc: string | null,
    fullAddress: string,
    house: string,
    pavilion: string | null,
    flat: string | null
    payment?: "online" | "courier"
}