# API de Relatório de Transações

Mesmo numa era digital, ainda é extremamente comum o lojista realizar o fechamento do caixa de maneira manual comparando as informações dos comprovantes emitidos no POS (aquela maquininha de cartão que imprimi o comprovante no momento da venda) com as informações geradas pelo seu sistema de vendas. Uma excelente forma de resolver esse problema é utilizar nosso sistema integrador TEF (Transferencias Eletrônicas de Fundos) para realizar as transações de pagamento a partir de um PDV integrado, através de nossos SDKs, com a automação comercial do cliente utilizando um PinPad (aquelas maquininhas que normalmente são vistas nos caixas de supermercado ou farmácias) para capturar os dados do cartão do consumidor.

A Cappta tem como propósito tornar a vida do empreendedor brasileiro mais fácil e para isso, buscamos prover soluções tecnologicas como a API de Relatório de Transações que é responsável por alimentar um portal onde o empreendedor é capaz de acompanhar todas as vendas realizadas, podendo filtrá-las por data, bandeira, adiquirente, entre outros filtros.

- ***Bandeira*** - são empresas que regulam o mercado de cartões de crédito, estabelecendo o padrão sob o qual as adquirentes devem processar seus cartões e a precificação dos diferentes estabelecimentos. São empresas como a VISA, Mastercard, Elo, entre outras.

- ***Adquirente*** - são membros licenciados que analisam e aceitam estabelecimentos em seus programas de cartões e processos de transações financeiras. São empresas como a Stone, Cielo, Rede, GetNet, entre outras.

## Qual é o desafio?

O seu desafio será implementar uma API Restful que seja capaz gerar relatórios em json para que possa ser consumida pelas automações comerciais que integram com nosso integrador TEF. Para isso será necessário que a API permita obter os relatórios filtrando-os por cnpj do lojista, data, bandeira ou adquirente e esses filtros poderão ser compostos por mais de um parâmetro, ou seja, deverá pemitir a busca pelas bandeiras VISA e Mastercard na mesma requisição e também deverá permitir a filtragem das vendas do cnpj realizadas pela bandeira Mastercard na data atual e as ultimas vendas realizadas pela adquirente Stone nos ultimos 30 dias.

*IMPORTANTE!* Como o volume de transações de pagamento realizadas ao longo do dia pode chegar à centenas de milhares de transações, o custo computacional para processar uma requisição deste tipo é bastante alto, sem contar que o relatório devolvido em json terá um tamanho muito grande, impactando na performance da entrega da resposta e também no consumo dos dados por parte dos clientes.

### O formato de resposta devolvido pela API deverá ser semelhante ao abaixo

``` json
{
    "results": [
        {
            "merchantCnpj": "77404852000179",
            "checkoutCode": 36245,
            "cipheredCardNumber": "************8082",
            "amountInCents": 1001,
            "installments": 1,
            "acquirerName": "Cielo",
            "paymentMethod": "Débito à Vista",
            "cardBrandName": "Elo Debito",
            "status": "Aprovada",
            "statusInfo": "Transação autorizada pela adquirente.",
            "CreatedAt": "2018-03-01T01:02:34",
            "AcquirerAuthorizationDateTime": "2018-03-01T01:02:38"
        },
        {
            "merchantCnpj": "30481457000126",
            "checkoutCode": 39206,
            "cipheredCardNumber": "************9077",
            "amountInCents": 3785,
            "installments": 1,
            "acquirerName": "Cielo",
            "paymentMethod": "Débito à Vista",
            "cardBrandName": "Elo Debito",
            "status": "Aprovada",
            "statusInfo": "Transação autorizada pela adquirente.",
            "CreatedAt": "2018-03-04:00:52",
            "AcquirerAuthorizationDateTime": "2018-03-04:00:52"
        },
        ...
    ]
}
```

### Os principais pontos que serão observados são:


- Qualidade do código
- Bom uso de SOLID
- Aplicação adequada de design patterns
- Domínio sobre Resful
- Domínio sobre a escrita de testes unitários
- Decisões tomadas em relação à arquitetura da API
- Cuidado com o versionamento do código

:nerd_face: :green_heart:
